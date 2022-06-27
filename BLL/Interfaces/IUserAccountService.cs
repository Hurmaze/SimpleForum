using Services.Models;

namespace Services.Interfaces
{
    /// <summary>
    /// Interface for the UserAccount service
    /// </summary>
    public interface IUserAccountService
    {
        /// <summary>
        /// Logins asynchronous.
        /// </summary>
        /// <param name="authModel">The authentication model.</param>
        /// <returns>Task&lt;string&gt; - JWT token</returns>
        public Task<string> LoginAsync(LoginModel authModel);

        /// <summary>
        /// Registers the Account asynchronous.
        /// </summary>
        /// <param name="authModel">The authentication model.</param>
        /// <returns>Task&lt;UserModel&gt;</returns>
        public Task<UserModel> RegisterAsync(RegistrationModel authModel);

        /// <summary>
        /// Creates the role if not exist.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;RoleModel&gt;</returns>
        public Task<RoleModel> CreateRoleIfNotExist(RoleModel model);

        /// <summary>
        /// Deletes the role asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task DeleteRoleAsync(int id);

        /// <summary>
        /// Changes the role asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        public Task ChangeRoleAsync(string email, int roleId);

        /// <summary>
        /// Gets the User by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;UserModel&gt;</returns>
        public Task<UserModel> GetByIdAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;UserModel&gt;&gt;.</returns>
        public Task<IEnumerable<UserModel>> GetAllAsync();

        /// <summary>
        /// Gets all roles asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;RoleModel&gt;&gt;.</returns>
        public Task<IEnumerable<RoleModel>> GetAllRolesAsync();

        /// <summary>
        /// Updates the User asynchronous.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        public Task UpdateAsync(UserModel userModel);

        /// <summary>
        /// Changes the nickname asynchronous.
        /// </summary>
        /// <param name="issuerEmail">The issuer email.</param>
        /// <param name="nickname">The nickname.</param>
        /// <returns></returns>
        public Task ChangeNicknameAsync(string issuerEmail, NicknameModel nickname);

        /// <summary>
        /// Deletes the Account by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task DeleteByIdAsync(int userId);
        
    }
}