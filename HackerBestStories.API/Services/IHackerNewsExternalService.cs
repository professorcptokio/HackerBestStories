using HackerBestStories.DTOs;

namespace HackerBestStories.API.Services
{
    public interface IHackerNewsExternalService
    {
        Task<int[]> GetBestStoriesIdsAsync();
        Task<HackerNewsStoryDto?> GetStoryAsync(int storyId);
    }
}
