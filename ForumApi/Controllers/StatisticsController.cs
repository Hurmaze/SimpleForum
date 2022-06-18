using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

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

        [HttpGet("threads/{count}")]
        public async Task<ActionResult<ForumThreadModel>> GetMostPopularThreads(int count = 3)
        {
            var post = await _statisticService.GetMostPopularThreadsAsync(count);

            return Ok(post);
        }

        [HttpGet("users/{count}")]
        public async Task<ActionResult<ForumThreadModel>> GetMostActiveUsers(int count = 3)
        {
            var post = await _statisticService.GetMostActiveUsersAsync(count);

            return Ok(post);
        }
    }
}
