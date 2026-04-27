namespace HackerBestStories.API.DTOs
{
       public record HackerNewsStoryDto(
         int Id,
         string? Title,
         string? Url,
         string? By,
         long Time,
         int Score,
         int Descendants);
}
