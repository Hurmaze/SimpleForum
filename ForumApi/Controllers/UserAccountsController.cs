using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;

        public UserAccountsController(IUserAccountService userAccountService)
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
        [Authorize(Roles = "admin, moderator")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var user = await _userAccountService.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpPut("{userId}/role/{roleId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ChangeRole(string email, int roleId)
        {
            await _userAccountService.ChangeRoleAsync(email, roleId);

            return NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(LoginModel model)
        {
            var token = await _userAccountService.LoginAsync(model);

            return Ok(new { access_token = token });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Register(RegistrationModel model)
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

        [HttpPost("changenickname")]
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
        public async Task<ActionResult> Update(UserModel model)
        {
            await _userAccountService.UpdateAsync(model);

            return NoContent();
        }
    }
}
