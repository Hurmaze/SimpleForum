using AutoMapper;
using Services.Interfaces;
using Services.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Services.Validation;
using DAL.Entities.Forum;
using DAL.Entities.Account;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Services.Validation.Exceptions;
using Services.Models;

namespace Services.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOptions<JwtOptions> _authOptions;
        private readonly ILogger<UserAccountService> _logger;
        public UserAccountService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtOptions> options, ILogger<UserAccountService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authOptions = options;
            _logger = logger;
        }

        public async Task ChangeRoleAsync(string email, int roleId)
        {
            var account = await _unitOfWork.AccountRepository.GetByEmailAsync(email);
            if(account == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Email", email.ToString()));
            }

            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId);

            if(role == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Role).Name, "Id", roleId.ToString()));
            }

            account.Role = role;

            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("The role of the user {email} has been changed to {rolename}", account.Email, role.RoleName);
        }

        public async Task<RoleModel> CreateRoleIfNotExist(RoleModel model)
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            var isExist = roles.Any(r => r.RoleName == model.RoleName);

            if (isExist)
            {
                throw new AlreadyExistException(String.Format(ExceptionMessages.AlreadyExists, typeof(Role).Name, "RoleName", model.RoleName));
            }

            var role = _mapper.Map<Role>(model);

            await _unitOfWork.RoleRepository.AddAsync(role);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("The role {rolename} is created.", role.RoleName);

            var roleView = _mapper.Map<RoleModel>(role);
            return roleView;
        }

        public async Task DeleteByIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if(user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", userId.ToString()));
            }

            var account = await _unitOfWork.AccountRepository.GetByEmailAsync(user.Email);

            await _unitOfWork.UserRepository.DeleteByIdAsync(userId);
            await _unitOfWork.AccountRepository.DeleteByIdAsync(account.Id);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("The user {email} has been deleted.", user.Email);
        }

        public async Task DeleteRoleAsync(int id)
        {
            var model = await _unitOfWork.RoleRepository.DeleteByIdAsync(id);

            await _unitOfWork.SaveAsync();

            if(model == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Role).Name, "Id", id.ToString()));
            }

            _logger.LogInformation("Role {rolename} has been deleted.", model.RoleName);
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if(user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", id.ToString()));
            }

            return _mapper.Map<UserModel>(user);
        }

        public async Task<IEnumerable<RoleModel>> GetAllRolesAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<RoleModel>>(roles);
        }

        public async Task<string> LoginAsync(LoginModel authModel)
        {
            var account = await _unitOfWork.AccountRepository.GetByEmailAsync(authModel.Email);

            if(account == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Email", authModel.Email.ToString()));
            }

            if(!VerifyPassword(authModel.Password, account.PasswordHash, account.PasswordSalt))
            {
                throw new WrongPasswordException(ExceptionMessages.WrongPassword);
            }

            _logger.LogInformation("The user with email {email} has logged into.", authModel.Email);
            return GenerateToken(_mapper.Map<AccountModel>(account));
        }

        public async Task<UserModel> RegisterAsync(RegistrationModel authModel)
        {
            bool isExist = await _unitOfWork.AccountRepository.IsEmailExistAsync(authModel.Email);

            if (isExist)
            {
                throw new InvalidRegistrationException(String.Format(ExceptionMessages.EmailIsAlreadyUsed, authModel.Email));
            }

            if(authModel.Nickname == "")
            {
                authModel.Nickname = null;
            }

            bool isTaken = await _unitOfWork.UserRepository.IsNicknameTakenAsync(authModel.Nickname);

            if (isTaken)
            {
                throw new NicknameTakenException(string.Format(ExceptionMessages.NicknameTaken, authModel.Nickname));
            }
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            var role = roles.FirstOrDefault(x => x.RoleName == BasicRoles.User.ToString());

            var accountModel = CreateAccount(authModel.Password, authModel, role.Id);

            var account = _mapper.Map<Account>(accountModel);
            account.Role = role;

            var user = _mapper.Map<User>(authModel);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User with email {email} registered.", authModel.Email);
            return _mapper.Map<UserModel>(user);
        }

        public async Task UpdateAsync(UserModel userModel)
        { 
            var user =  await _unitOfWork.UserRepository.GetByEmailAsync(userModel.Email);

            if(user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Email", userModel.Email.ToString()));
            }

            if(user.Nickname != userModel.Nickname)
            {
                bool isTaken = await _unitOfWork.UserRepository.IsNicknameTakenAsync(userModel.Nickname);

                if (isTaken)
                {
                    throw new NicknameTakenException(string.Format(ExceptionMessages.NicknameTaken, userModel.Nickname));
                }
            }

            user = _mapper.Map(userModel, user);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("The user with email {email} is updated.", user.Email);
        }

        public async Task ChangeNicknameAsync(string issuerEmail, NicknameModel nickname)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(issuerEmail);

            if (user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Email", issuerEmail.ToString()));
            }

            if (user.Nickname == nickname.Nickname)
            {
                throw new NicknameTakenException(ExceptionMessages.NicknamesAreEqual);
            }

            bool isTaken = await _unitOfWork.UserRepository.IsNicknameTakenAsync(nickname.Nickname);

            if (isTaken)
            {
                throw new NicknameTakenException(string.Format(ExceptionMessages.NicknameTaken, nickname.Nickname));
            }

            user.Nickname = nickname.Nickname;

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("The user with email {email} has changed the nickname to {nickname}.", user.Email, nickname.Nickname);
        }

        private AccountModel CreateAccount(string password, RegistrationModel registrationModel, int roleId)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            AccountModel accountModel = new AccountModel {
                Email = registrationModel.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = roleId
            };

            return accountModel;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string GenerateToken(AccountModel user)
        {
            var authParams = _authOptions.Value;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName)
            };
            claims.Add(new Claim("id",user.Id.ToString()));    

            var key = authParams.GetSymmetricSecurityKey();
            var credantials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credantials);

            _logger.LogInformation("JWT token for {email} generated.", user.Email);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
