using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostModel>>> Get()
        {
            var posts = await _postService.GetAllAsync();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostModel>> Get(int id)
        {
            var post = await _postService.GetByIdAsync(id);

            return Ok(post);
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<PostModel>> GetByuserId(int userId)
        {
            var posts = await _postService.GetPostsByUserIdAsync(userId);

            return Ok(posts);
        }

        [HttpDelete("{id}")]
        [Authorize( Roles = "admin, moderator")]
        public async Task<ActionResult> Delete(int id)
        {
            await _postService.DeleteByIdAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin, moderator")]
        public async Task<ActionResult> Update(PostModel model)
        {
            await _postService.UpdateAsync(model);

            return NoContent(); 
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add(PostModel model)
        {
            var created = await _postService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }
    }
}
