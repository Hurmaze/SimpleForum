using ForumApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using Services.Validation.Exceptions;
using System.Threading.Tasks;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokensController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Generating JWT token.
        /// </summary>
        /// <param name="model">The LoginModel</param>
        /// <returns></returns>
        /// <response code="200">Returns the JWT token</response>
        /// <response code="400">Password is incorrect. </response>
        /// <response code="404">The user not found. </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<string>> Token(LoginRequest model)
        {
            var token = await _tokenService.GetTokenAsync(model);

            return Ok(new { access_token = token });
        }

        public async Task<ActionResult<string>> RefreshToken()
        {
            return Ok();
        }
    }
}
