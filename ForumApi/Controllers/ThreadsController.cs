﻿using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly IForumThreadService _forumThreadService;

        public ThreadsController(IForumThreadService forumThreadService)
        {
            _forumThreadService = forumThreadService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumThreadModel>>> Get()
        {
            var threads = await _forumThreadService.GetAllAsync();

            return Ok(threads);
        }

        [HttpGet("/users/{id}")]
        public async Task<ActionResult<IEnumerable<ForumThreadModel>>> GetByUserId(int id)
        {
            var threads = await _forumThreadService.GetThreadsByUserIdAsync(id);

            return Ok(threads);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ForumThreadModel>> GetById(int id)
        {
            var post = await _forumThreadService.GetByIdAsync(id);

            return Ok(post);
        }

        [HttpGet("{id}/posts")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetThreadPosts(int id)
        {
            var posts = await _forumThreadService.GetThreadPostsAsync(id);

            return Ok(posts);
        }

        [HttpPost]
        public async Task<ActionResult> Add(ForumThreadModel model)
        {
            var created = await _forumThreadService.AddAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        [HttpPost("/themes")]
        public async Task<ActionResult> AddTheme(ThemeModel model)
        {
            var created = await _forumThreadService.AddNewThemeAsync(model);

            return CreatedAtAction(nameof(Add), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _forumThreadService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}