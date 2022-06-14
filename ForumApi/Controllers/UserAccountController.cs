using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllAsync()
        {
            var users = await _userAccountService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("roles")]
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

        [HttpPut("{id}/nickname")]
        public async Task<ActionResult> ChangeNickName(UserModel model, string nickname)
        {
            await _userAccountService.ChangeNickNameAsync(model, nickname);

            return NoContent();
        }

        [HttpPut("{userId}/role/{roleId}")]
        public async Task<ActionResult> ChangeRole(string email, int roleId)
        {
            await _userAccountService.ChangeUserRoleAsync(email, roleId);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginModel model)
        {
            var token = await _userAccountService.LoginAsync(model);

            return Ok(token);
        }


        [HttpPost]
        public async Task<ActionResult<UserModel>> Register(RegistrationModel model)
        {
            var user = await _userAccountService.RegisterAsync(model);

            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }

        [HttpPost("roles")]
        public async Task<ActionResult<RoleModel>> CreateRole(RoleModel model)
        {
            var role = await _userAccountService.CreateRoleIfNotExist(model);

            return CreatedAtAction(nameof(CreateRole), new { id = role.Id }, role);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userAccountService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpDelete("roles/{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            await _userAccountService.DeleteRoleAsync(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UserModel model)
        {
            await _userAccountService.UpdateAsync(model);

            return NoContent();
        }
    }
}
