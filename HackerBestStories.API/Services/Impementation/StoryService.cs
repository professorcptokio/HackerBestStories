using HackerBestStories.DTOs;

namespace HackerBestStories.API.Services.Impementation
{
    public class StoryService : IStoryService
    {
        private const int MinCount = 1;
        private const int MaxCount = 500;
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private readonly IHackerNewsExternalService _externalService;
        private readonly ILogger<StoryService> _logger;

        public StoryService(
            IHackerNewsExternalService externalService,
            ILogger<StoryService> logger)
        {
            _externalService = externalService ?? throw new ArgumentNullException(nameof(externalService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<GetStoriesResponse>> GetBestStoriesAsync(int count)
        {
            if (count < MinCount || count > MaxCount)
            {
                throw new ArgumentException(
                    $"Story count must be between {MinCount} and {MaxCount}.",
                    nameof(count));
            }

            var storyIds = await GetBestStoriesIdsAsync();
            var topIds = storyIds.Take(count).ToArray();
            var stories = await FetchStoriesAsync(topIds);

            return stories
                .OrderByDescending(s => s.Score)
                .ToList();
        }

        private async Task<int[]> GetBestStoriesIdsAsync()
        {            
            return await _externalService.GetBestStoriesIdsAsync();
        }

        private async Task<List<GetStoriesResponse>> FetchStoriesAsync(int[] storyIds)
        {
            var tasks = storyIds.Select(id => FetchStoryAsync(id));
            var results = await Task.WhenAll(tasks);

            return results
                .Where(s => s is not null)
                .Cast<GetStoriesResponse>()
                .ToList();
        }

        private async Task<GetStoriesResponse?> FetchStoryAsync(int storyId)
        {
            try
            {
                var dto = await _externalService.GetStoryAsync(storyId);

                if (dto is null)
                    return null;

                return new GetStoriesResponse(
                    Title: dto.Title ?? string.Empty,
                    Uri: dto.Url ?? string.Empty,
                    PostedBy: dto.By ?? string.Empty,
                    Time: UnixEpoch.AddSeconds(dto.Time),
                    Score: dto.Score,
                    CommentCount: dto.Descendants);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to fetch story {StoryId}: {Message}", storyId, ex.Message);
                return null;
            }
        }
    }
}
