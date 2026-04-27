using HackerBestStories.API.DTOs;
using HackerBestStories.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerBestStories.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController(IStoryService storyService, ILogger<StoriesController> logger) : ControllerBase
    {

        /// <summary>
        /// Get the best stories from Hacker News API
        /// </summary>
        /// <param name="numberOfStories">Number of stories to retrieve (1-500)</param>
        /// <returns>List of stories sorted by score in descending order</returns>
        [HttpGet("{numberOfStories}", Name = "GetBestStories")]
        [Produces("application/json")]
        public async Task<ActionResult<List<GetStoriesResponse>>> GetStories(
            [FromRoute] int numberOfStories)
        {
            try
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("GetStories requested with count: {Count}", numberOfStories);
                }
                var stories = await storyService.GetBestStoriesAsync(numberOfStories);
                return Ok(stories);
            }
            catch (ArgumentException ex)
            {
                logger.LogWarning("Invalid request: {Message}", ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError("Unexpected error: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { error = "An unexpected error occurred while fetching stories" });
            }
        }
    }
}
