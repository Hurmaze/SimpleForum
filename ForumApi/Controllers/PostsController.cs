using Services.Interfaces;
using Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Validation.Exceptions;
using ForumApi.Models;

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

        /// <summary>
        /// Gets a list of all PostModel.
        /// </summary>
        /// <returns>The list of PostModel. </returns>
        /// <response code="200">Returns the list of PostModel.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostModel>))]
        public async Task<ActionResult<IEnumerable<PostModel>>> Get()
        {
            var posts = await _postService.GetAllAsync();

            return Ok(posts);
        }

        /// <summary>
        /// Gets the PostModel by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <response code="200">Returns the specified PostModel. </response>
        /// <response code="404">The PostModel not found. </response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<PostModel>> Get(int id)
        {
            var post = await _postService.GetByIdAsync(id);

            return Ok(post);
        }

        /// <summary>
        /// Gets a list of PostModel by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of user`s PostModel. </returns>
        /// <response code="200">Returns the list of users`s PostModel. </response>
        /// <response code="404">The user not found. </response>
        [HttpGet("users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetByUserId(int userId)
        {
            var posts = await _postService.GetPostsByUserIdAsync(userId);

            return Ok(posts);
        }

        /// <summary>
        /// Deletes the PostModel by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404">The PostModel not found. </response>
        [HttpDelete("{id}")]
        [Authorize( Roles = "admin, moderator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> Delete(int id)
        {
            await _postService.DeleteByIdAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Updates the PostModel by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The PostRequest.</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404">The PostModel not found. </response>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, moderator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> Update(int id, PostRequest model)
        {
            await _postService.UpdateAsync(id, model);

            return NoContent(); 
        }

        /// <summary>
        /// Creates new PostModel.
        /// </summary>
        /// <param name="model">The PostRequest. </param>
        /// <returns>The created PostModel. </returns>
        /// <response code="201">Returns the created PostModel. </response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostModel))]
        public async Task<ActionResult> Add(PostRequest model)
        {
            var created = await _postService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }
    }
}
