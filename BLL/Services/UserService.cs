using AutoMapper;
using Services.Interfaces;
using Services.Models;
using DAL.Interfaces;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Services.Validation.Exceptions;
using DAL.Entities;

namespace Services.Services
{
    /// <summary>
    /// UserAccount service
    /// </summary>
    /// <seealso cref="IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserService> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Changes the role asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <exception cref="NotFoundException"></exception>
        public async Task ChangeRoleAsync(int userId, int roleId)
        {
            var credentials = await _unitOfWork.CredentialsRepository.GetByUserIdAsync(userId);
            if(credentials == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", userId.ToString()));
            }

            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId);

            if(role == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(Role).Name, "Id", roleId.ToString()));
            }

            credentials.Role = role;
            credentials.RoleId = role.Id;

            _unitOfWork.CredentialsRepository.Update(credentials);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("The role of the user {email} has been changed to {rolename}", credentials.User.Email, role.RoleName);
        }

        /// <summary>
        /// Creates the role if not exist.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Task&lt;RoleModel&gt;
        /// </returns>
        /// <exception cref="AlreadyExistException"></exception>
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

        /// <summary>
        /// Deletes the Account by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="NotFoundException"></exception>
        public async Task DeleteByIdAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if(user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", userId.ToString()));
            }

            await _unitOfWork.UserRepository.DeleteByIdAsync(userId);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("The user {email} has been deleted.", user.Email);
        }

        /// <summary>
        /// Deletes the role asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="NotFoundException"></exception>
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

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Task&lt;IEnumerable&lt;UserModel&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        { 
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        /// <summary>
        /// Gets the User by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task&lt;UserModel&gt;
        /// </returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if(user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Id", id.ToString()));
            }

            return _mapper.Map<UserModel>(user);
        }

        /// <summary>
        /// Gets the users by role asynchronous.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserModel>> GetByRoleAsync(int roleId)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserModel>>(users);
        }

        /// <summary>
        /// Gets all roles asynchronous.
        /// </summary>
        /// <returns>
        /// Task&lt;IEnumerable&lt;RoleModel&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<RoleModel>> GetAllRolesAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<RoleModel>>(roles);
        }

        /// <summary>
        /// Registers the Account asynchronous.
        /// </summary>
        /// <param name="authModel">The authentication model.</param>
        /// <returns>
        /// Task&lt;UserModel&gt;
        /// </returns>
        /// <exception cref="InvalidRegistrationException"></exception>
        /// <exception cref="NicknameTakenException"></exception>
        public async Task<UserModel> RegisterAsync(RegistrationModel authModel)
        {
            bool isExist = await _unitOfWork.UserRepository.IsEmailExistAsync(authModel.Email);

            if (isExist)
            {
                throw new InvalidRegistrationException(String.Format(ExceptionMessages.EmailUsed, authModel.Email));
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

            var role = roles.FirstOrDefault(x => x.RoleName == BasicRoles.User.ToString().ToLower());

            var user = CreateAccount(authModel, role.Id);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("User with email {email} registered.", authModel.Email);
            return _mapper.Map<UserModel>(user);
        }

        /// <summary>
        /// Updates the User asynchronous.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <exception cref="Services.Validation.Exceptions.NotFoundException"></exception>
        /// <exception cref="Services.Validation.Exceptions.NicknameTakenException"></exception>
        public async Task UpdateAsync(int id, UserModel userModel)
        { 
            var user =  await _unitOfWork.UserRepository.GetByIdAsync(id);

            if(user == null)
            {
                throw new NotFoundException(String.Format(ExceptionMessages.NotFound, typeof(User).Name, "Email", userModel.Email.ToString()));
            }

            if(user.Email != userModel.Email)
            {
                throw new DifferenceEmailException(String.Format(ExceptionMessages.DifferenceEmail, user.Email, id));
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

        /// <summary>
        /// Changes the nickname asynchronous.
        /// </summary>
        /// <param name="issuerEmail">The issuer email.</param>
        /// <param name="nickname">The nickname.</param>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="NicknameTakenException"></exception>
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

        /// <summary>
        /// Creates the account.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="registrationModel">The registration model.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        private User CreateAccount(RegistrationModel registrationModel, int roleId)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registrationModel.Password));
            }

            User user = new User {
                Email = registrationModel.Email,
                Nickname = registrationModel.Nickname,
                Credentials = new Credentials
                {
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleId = roleId
                }
            };

            return user;
        }
    }
}
