using HackerBestStories.API.DTOs;

namespace HackerBestStories.API.Services
{
    public interface IHackerNewsExternalService
    {
        Task<int[]> GetBestStoriesIdsAsync();
        Task<HackerNewsStoryDto?> GetStoryAsync(int storyId);
    }
}
