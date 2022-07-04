using Services.Interfaces;
using Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userAccountService;

        public UsersController(IUserService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpGet]
        [Authorize(Roles = "admin, moderator")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllAsync()
        {
            var users = await _userAccountService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("roles")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetAllRolesAsync()
        {
            var roles = await _userAccountService.GetAllRolesAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var user = await _userAccountService.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpGet("roles/{id}")]
        public async Task<ActionResult<UserModel>> GetByRole(int id)
        {
            var user = await _userAccountService.GetByRoleAsync(id);

            return Ok(user);
        }

        [HttpPut("{userId}/role/{roleId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ChangeRole(int userId, int roleId)
        {
            await _userAccountService.ChangeRoleAsync(userId, roleId);

            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Register([FromBody] RegistrationModel model)
        {
            var user = await _userAccountService.RegisterAsync(model);

            return CreatedAtAction(nameof(Register), new { id = user.Id, }, user);
        }


        [HttpPost("roles")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<RoleModel>> CreateRole(RoleModel model)
        {
            var role = await _userAccountService.CreateRoleIfNotExist(model);

            return CreatedAtAction(nameof(CreateRole), new { id = role.Id }, role);
        }

        [HttpPut("nickname")]
        [Authorize]
        public async Task<ActionResult<NicknameModel>> ChangeNickname(NicknameModel model)
        {
            var email = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;

            await _userAccountService.ChangeNicknameAsync(email, model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userAccountService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpDelete("roles/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            await _userAccountService.DeleteRoleAsync(id);

            return NoContent();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update(int id,[FromBody] UserModel model)
        {
            await _userAccountService.UpdateAsync(id, model);

            return NoContent();
        }
    }
}
