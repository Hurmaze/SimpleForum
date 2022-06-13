using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserAccountService
    {
        public Task<string> LoginAsync(LoginModel authModel);
        public Task<UserModel> RegisterAsync(RegistrationModel authModel); 
        public Task CreateRoleIfNotExist(string roleName);
        public Task DeleteRoleAsync(int id);
        public Task ChangeNickNameAsync(UserModel userModel, string nickName);
        public Task ChangeUserRoleAsync(int userId, int roleId);
        public Task<UserModel> GetByIdAsync(int id);
        public Task<IEnumerable<UserModel>> GetAllAsync();
        public Task UpdateAsync(UserModel userModel);
        public Task DeleteByIdAsync(int userId);
        
    }
}