using Services.Models;

namespace Services.Interfaces
{
    public interface IUserAccountService
    {
        public Task<string> LoginAsync(LoginModel authModel);
        public Task<UserModel> RegisterAsync(RegistrationModel authModel); 
        public Task<RoleModel> CreateRoleIfNotExist(RoleModel model);
        public Task DeleteRoleAsync(int id);
        public Task ChangeRoleAsync(string email, int roleId);
        public Task<UserModel> GetByIdAsync(int id);
        public Task<IEnumerable<UserModel>> GetAllAsync();
        public Task<IEnumerable<RoleModel>> GetAllRolesAsync();
        public Task UpdateAsync(UserModel userModel);
        public Task ChangeNicknameAsync(string issuerEmail, NicknameModel nickname);
        public Task DeleteByIdAsync(int userId);
        
    }
}