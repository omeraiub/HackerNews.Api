using HackerNews.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[ResponseCache(CacheProfileName = "Default30")]
public class HackerNewsController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;

    public HackerNewsController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("TopStories")]
    public async Task<List<StoryDto>> GetHackerNewsAsync([FromQuery] int numberOfStories)
    {
        if(numberOfStories <= 0)
        {
            return new List<StoryDto>();
        }
        var stories = await _hackerNewsService.GetHackerNewsAsync(numberOfStories);
        return stories.Select(s => new StoryDto
        {
            Title = s.Title,
            Url = s.Url,
            PostedBy = s.By,
            Time = s.Time,
            Score = s.Score,
            CommentCount = s.Descendants,

        }).ToList();
    }
}
