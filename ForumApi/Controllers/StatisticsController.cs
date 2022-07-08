using Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        /// <summary>
        /// Gets the most popular threads.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The list of most popular threads. </returns>
        /// <response code="200">Returns the list of most popular threads. </response>
        [HttpGet("threads/popular/{count}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ForumThreadModel>))]
        public async Task<ActionResult<IEnumerable<ForumThreadModel>>> GetMostPopularThreads(int count = 3)
        {
            var threads = await _statisticService.GetMostPopularThreadsAsync(count);

            return Ok(threads);
        }

        /// <summary>
        /// Gets the most active users.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The list of most active users. </returns>
        /// <response code="200">Returns the list of most active users. </response>
        [HttpGet("users/active/{count}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserModel>))]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetMostActiveUsers(int count = 3)
        {
            var users = await _statisticService.GetMostActiveUsersAsync(count);

            return Ok(users);
        }
    }
}
