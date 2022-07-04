using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Services;
using System.Threading.Tasks;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public TokensController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<string> Token(LoginModel model)
        {
            return await _tokenService.GetTokenAsync(model);
        }
    }
}
