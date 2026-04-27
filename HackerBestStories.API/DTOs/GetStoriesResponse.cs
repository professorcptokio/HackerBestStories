namespace HackerBestStories.API.DTOs
{ 
    public record GetStoriesResponse(
        string Title,
        string Uri,
        string PostedBy,
        DateTime Time,
        int Score,
        int CommentCount);
}
