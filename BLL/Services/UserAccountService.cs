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

            var role = await _unitOfWork.AccountRepository.GetRoleByIdAsync(roleId);

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

        public Task CreateRoleIfNotExist(string roleName)
        {
            
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
            await 
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        public Task<IEnumerable<PostModel>> GetAllUserPostsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            return _mapper.Map<UserModel>(user);
        }

        public Task<string> LoginAsync(LoginModel authModel)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> RegisterAsync(RegistrationModel authModel)
        {
            

            if (!IsValid(authModel))
            {
                throw new InvalidRegistrationException(string.Format(ExceptionMessages.InvalidRegistration));
            }

            var accountModel = CreateAccount(authModel.Password, authModel);

            var account = _mapper.Map<Account>(accountModel);

            var user = _mapper.Map<User>(authModel);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserModel>(user);
        }

        public Task UpdateAsync(UserModel user)
        {
            throw new NotImplementedException();
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

        private string GenerateToken(RegistrationModel user)
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //TODO: do
        public async Task<bool> IsValid(RegistrationModel registrationModel)
        {
            if (registrationModel == null)
            {
                throw new InvalidRegistrationException(ExceptionMessages.InvalidRegistration);
            }

            bool isExist = await _unitOfWork.AccountRepository.IsEmailExist(registrationModel.Email);

            if (isExist)
            {
                throw new EmailIsUsedException(string.Format(ExceptionMessages.EmailIsAlreadyUsed, registrationModel.Email));
            }

            if (!(registrationModel != null && registrationModel.Password.Trim() != ""))
            {
                return false;
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidated = hasNumber.IsMatch(registrationModel.Password) && hasUpperChar.IsMatch(registrationModel.Password) && hasMinimum8Chars.IsMatch(registrationModel.Password);

            if (!isValidated)
            {
                return false;
            }

            if(registrationModel.)

            return true;
        }
    }
}
