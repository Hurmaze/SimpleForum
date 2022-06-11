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
                _logger.LogWarning(string.Format(ExceptionMessages.NicknameTaken, nickName));
                throw new NicknameTakenException(string.Format(ExceptionMessages.NicknameTaken, nickName));
            }

            userModel.Nickname = nickName;

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userModel.Id);

            user = _mapper.Map(userModel, user);

            _unitOfWork.UserRepository.Update(user);

            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"User {userModel.Email} has changed nickname to {nickName}");
        }

        public async Task ChangeUserRoleAsync(int userId, int roleId)
        {
            var user = await _unitOfWork.AccountRepository.GetByIdAsync(userId);
            if(user == null)
            {
                _logger.LogWarning(String.Format(ExceptionMessages.NotFound, typeof(User), "Id", userId.ToString()));
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User), "Id", userId.ToString()));
            }

            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId);

            if(role == null)
            {
                _logger.LogWarning(String.Format(ExceptionMessages.NotFound, typeof(Role), "Id", roleId.ToString()));
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Role), "Id", roleId.ToString()));
            }

            user.Role = role;

            _unitOfWork.AccountRepository.Update(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"The role of the user {user.Email} has been changed to {role.RoleName}");
        }

        public async Task CreateRoleIfNotExist(string roleName)
        {
            if(roleName == null || roleName.Trim().Length == 0)
            {
                _logger.LogWarning("Role name is empty.");
                throw new ArgumentNullException($"Role name is empty.");
            }
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            var isExist = roles.Any(r => r.RoleName == roleName);

            if (isExist)
            {
                _logger.LogWarning(String.Format(ExceptionMessages.AlreadyExists, typeof(Role), "RoleName", roleName));
                throw new AlreadyExistException(String.Format(ExceptionMessages.AlreadyExists, typeof(Role), "RoleName", roleName));
            }

            Role role = new Role { RoleName = roleName };

            await _unitOfWork.RoleRepository.AddAsync(role);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"The role {role.RoleName} is created.");
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if(user == null)
            {
                _logger.LogWarning(String.Format(ExceptionMessages.NotFound, typeof(User), "Id", userId.ToString()));
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User), "Id", userId.ToString()));
            }

            var account = await _unitOfWork.AccountRepository.GetByEmailAsync(user.Email);

            await _unitOfWork.UserRepository.DeleteByIdAsync(userId);
            await _unitOfWork.AccountRepository.DeleteByIdAsync(account.Id);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"The user {user.Email} has been deleted.");
        }

        public async Task DeleteRoleAsync(int id)
        {
            await _unitOfWork.RoleRepository.DeleteByIdAsync(id);
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

        public async Task<string> LoginAsync(LoginModel authModel)
        {
            var account = await _unitOfWork.AccountRepository.GetByEmailAsync(authModel.Email);

            if(account == null)
            {
                _logger.LogWarning(String.Format(ExceptionMessages.NotFound, typeof(User), "Email", authModel.Email.ToString()));
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User), "Email", authModel.Email.ToString()));
            }

            if(!VerifyPassword(authModel.Password, account.PasswordHash, account.PasswordSalt))
            {
                _logger.LogWarning(ExceptionMessages.WrongPassword);
                throw new WrongPasswordException(ExceptionMessages.WrongPassword);
            }

            return GenerateToken(_mapper.Map<AccountModel>(account));
        }

        public async Task<UserModel> RegisterAsync(RegistrationModel authModel)
        {
            bool isExist = await _unitOfWork.AccountRepository.IsEmailExist(authModel.Email);

            if (isExist)
            {
                _logger.LogWarning(String.Format(ExceptionMessages.EmailIsAlreadyUsed, authModel.Email));
                throw new InvalidRegistrationException(String.Format(ExceptionMessages.EmailIsAlreadyUsed, authModel.Email));
            }

            var accountModel = CreateAccount(authModel.Password, authModel);

            var account = _mapper.Map<Account>(accountModel);

            var user = _mapper.Map<User>(authModel);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"User with email {authModel.Email} registered.");
            return _mapper.Map<UserModel>(user);
        }

        public async Task UpdateAsync(UserModel userModel)
        { 
            var user =  await _unitOfWork.UserRepository.GetByEmail(userModel.Email);

            if(user == null)
            {
                _logger.LogWarning(String.Format(ExceptionMessages.NotFound, typeof(User), "Email", userModel.Email.ToString()));
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User), "Email", userModel.Email.ToString()));
            }

            user = _mapper.Map(userModel, user);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation($"The user with email {user.Email} is updated.");
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

            _logger.LogInformation($"JWT token for {user.Email} generated.");

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
