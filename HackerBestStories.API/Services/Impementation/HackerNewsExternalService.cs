using HackerBestStories.DTOs;

namespace HackerBestStories.API.Services.Impementation
{
    public class HackerNewsExternalService : IHackerNewsExternalService
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;
        private readonly ILogger<HackerNewsExternalService> logger;

        public HackerNewsExternalService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<HackerNewsExternalService> logger)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.baseUrl = configuration["ExternalServices:HackerNewsBaseUrl"]
                ?? "https://hacker-news.firebaseio.com/v0";
        }

        public async Task<int[]> GetBestStoriesIdsAsync()
        {
            var url = $"{baseUrl}/beststories.json";
            return await this.httpClient.GetFromJsonAsync<int[]>(url) ?? Array.Empty<int>();
        }

        public async Task<HackerNewsStoryDto?> GetStoryAsync(int storyId)
        {
            try
            {
                var url = $"{baseUrl}/item/{storyId}.json";
                return await this.httpClient.GetFromJsonAsync<HackerNewsStoryDto>(url);
            }
            catch (Exception exception)
            {
                this.logger.LogWarning("Failed to fetch story {StoryId}: {Message}", storyId, exception.Message);
                return null;
            }
        }
    }
}
