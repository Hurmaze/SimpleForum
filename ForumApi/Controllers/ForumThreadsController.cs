using Services.Interfaces;
using Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services.Validation.Exceptions;
using ForumApi.Models;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumThreadsController : ControllerBase
    {
        private readonly IForumThreadService _forumThreadService;

        public ForumThreadsController(IForumThreadService forumThreadService)
        {
            _forumThreadService = forumThreadService;
        }

        /// <summary>
        /// Gets a list of all ForumThreadModel
        /// </summary>
        /// <returns>The list of ForumThreadModel </returns>
        /// <response code="200">Returns the items</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ForumThreadModel>))]
        public async Task<ActionResult<IEnumerable<ForumThreadModel>>> Get()
        {
            var threads = await _forumThreadService.GetAllAsync();

            return Ok(threads);
        }

        /// <summary>
        /// Gets a list of all ThemeModel.
        /// </summary>
        /// <returns>The list of the ThemeModel. </returns>
        /// <response code="200">Returns the items. </response>
        [HttpGet("themes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ThemeModel>))]
        public async Task<ActionResult<IEnumerable<ForumThreadModel>>> GetThemes()
        {
            var themes = await _forumThreadService.GetAllThemesAsync();

            return Ok(themes);
        }

        /// <summary>
        /// Gets a list of ForumThreadModel by the user identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <response code="200">Returns the items. </response>
        /// <response code="404">The user not found. </response>
        [HttpGet("users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ForumThreadModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<ForumThreadModel>>> GetByUserId(int id)
        {
            var threads = await _forumThreadService.GetThreadsByUserIdAsync(id);

            return Ok(threads);
        }

        /// <summary>
        /// Gets the list of ForumThreadModel by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The list of the ForumThreadModel. </returns>
        /// <response code="200">Returns the item. </response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForumThreadModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ForumThreadModel>> GetById(int id)
        {
            var post = await _forumThreadService.GetByIdAsync(id);

            return Ok(post);
        }

        /// <summary>
        /// Gets the list of PostModel by thread identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The list of PostModel. </returns>
        /// <response code="200">Returns the items. </response>
        /// <response code="404">Not found. </response>
        [HttpGet("{id}/posts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetThreadPosts(int id)
        {
            var posts = await _forumThreadService.GetThreadPostsAsync(id);

            return Ok(posts);
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The ForumThreadRequest.</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404">The ForumThreadModel not found. </response>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin, moderator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> Update(int id, [FromBody]ForumThreadRequest model)
        {
            await _forumThreadService.UpdateAsync(id, model);

            return NoContent();
        }

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The ForumThreadRequest. </param>
        /// <returns></returns>
        /// <response code="201">Returns the created FroumThreadModel. </response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ForumThreadModel))]
        public async Task<ActionResult> Add(ForumThreadRequest model)
        {
            var created = await _forumThreadService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        /// <summary>
        /// Adds the theme.
        /// </summary>
        /// <param name="model">The ThemeModel.</param>
        /// <returns>The created ThemeModel. </returns>
        /// <response code="201">Returns the created ThemeModel. </response>
        /// <response code="400">The ThemeModel is already exists. </response>
        [HttpPost("themes")]
        [Authorize(Roles = "admin, moderator")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ThemeModel>> AddTheme([FromBody]ThemeModel model)
        {
            var created = await _forumThreadService.AddThemeAsync(model);

            return CreatedAtAction(nameof(AddTheme), new { id = created.Id }, created);
        }

        /// <summary>
        /// Deletes the theme.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404">The ThemeModel not found. </response>
        [HttpDelete("themes/{id}")]
        [Authorize(Roles = "admin, moderator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> DeleteTheme(int id)
        {
            await _forumThreadService.DeleteThemeByIdAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Deletes the ForumThreadModel by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <response code="204"></response>
        /// <response code="404">The ForumThreadModel not found. </response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, moderator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult> Delete(int id)
        {
            await _forumThreadService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}
