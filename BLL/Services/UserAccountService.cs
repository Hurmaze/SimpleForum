using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using BLL.Validation;
using DAL.Entities.Forum;
using DAL.Entities.Account;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace BLL.Services
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

        public async Task ChangeNickNameAsync(UserModel userModel, string nickName)
        {
            bool isTaken = await _unitOfWork.UserRepository.IsNicknameTaken(nickName);

            if (isTaken)
            {
                throw new NicknameTakenException(string.Format(ExceptionMessages.NicknameTaken, nickName));
            }

            userModel.Nickname = nickName;

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userModel.Id);

            user = _mapper.Map(userModel, user);

            _unitOfWork.UserRepository.Update(user);

            await _unitOfWork.SaveAsync();
            _logger.LogInformation("User {email} has changed nickname to {nickname}", userModel.Email, nickName);
        }

        public async Task ChangeUserRoleAsync(string email, int roleId)
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
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", id.ToString()));
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
            bool isExist = await _unitOfWork.AccountRepository.IsEmailExist(authModel.Email);

            if (isExist)
            {
                throw new InvalidRegistrationException(String.Format(ExceptionMessages.EmailIsAlreadyUsed, authModel.Email));
            }

            var accountModel = CreateAccount(authModel.Password, authModel);

            var account = _mapper.Map<Account>(accountModel);

            var user = _mapper.Map<User>(authModel);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User with email {email} registered.", authModel.Email);
            return _mapper.Map<UserModel>(user);
        }

        public async Task UpdateAsync(UserModel userModel)
        { 
            var user =  await _unitOfWork.UserRepository.GetByEmail(userModel.Email);

            if(user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Email", userModel.Email.ToString()));
            }

            user = _mapper.Map(userModel, user);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("The user with email {email} is updated.", user.Email);
        }

        private AccountModel CreateAccount(string password, RegistrationModel registrationModel)
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
                RoleName = registrationModel.RoleName
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

            var key = authParams.GetSymmetricSecurityKey();
            var credantials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credantials);

            _logger.LogInformation("JWT token for {email} generated.", user.Email);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
       
    }
}
