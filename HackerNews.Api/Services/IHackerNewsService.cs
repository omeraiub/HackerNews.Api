namespace HackerNews.Api.Services;

public interface IHackerNewsService
{
    Task<List<Story>> GetHackerNewsAsync(int n);
}