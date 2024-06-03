namespace HackerNews.Api.Controllers;

public class StoryDto
{
    public string Title { get; internal set; }
    public string Url { get; internal set; }
    public string PostedBy { get; internal set; }
    public DateTimeOffset Time { get; internal set; }
    public int Score { get; internal set; }
    public int CommentCount { get; internal set; }
}