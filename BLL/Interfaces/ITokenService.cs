using Services.Models;

namespace Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync(LoginRequest login);
    }
}
