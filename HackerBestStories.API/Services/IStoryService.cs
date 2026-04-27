using HackerBestStories.DTOs;

namespace HackerBestStories.API.Services
{
    public interface IStoryService
    {
        Task<List<GetStoriesResponse>> GetBestStoriesAsync(int count);
    }
}
