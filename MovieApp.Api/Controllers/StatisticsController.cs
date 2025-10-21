using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.DTOs;
using MovieApp.Application.Interfaces;

namespace MovieApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Retrieves statistical data about movies and actrors.
        /// </summary>
        /// <remarks>
        /// Includes metrics such as total counts, average ratings, top genres, and age ranges.
        /// </remarks>
        /// <response code="200">Statistics calculated successfully.</response>
        /// <response code="500">Internal server error while processing the statistics.</response>
        [HttpGet]
        public async Task<ActionResult<StatisticsResult>> GetStatistics()
        {
            var result = await _statisticsService.GetStatisticsAsync();
            return Ok(result);
        }
    }
}
