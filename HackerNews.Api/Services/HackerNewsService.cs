using System.Net.Http;
using System.Text.Json.Serialization;

namespace HackerNews.Api.Services;

public class HackerNewsService : IHackerNewsService
{
    private readonly HttpClient _httpClient;

    public HackerNewsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Story>> GetHackerNewsAsync(int n)
    {

        var bestStoryIds = await _httpClient.GetFromJsonAsync<List<int>>("topstories.json");


        var stories = new List<Story>();
        foreach (var storyId in bestStoryIds.Take(n))
        {
            var storyResponse = await _httpClient.GetAsync($"item/{storyId}.json");
            storyResponse.EnsureSuccessStatusCode();
            var story = await storyResponse.Content.ReadFromJsonAsync<Story>();
            stories.Add(story);
        }

        // Sort stories by score in descending order
        stories = stories.OrderByDescending(s => s.Score).ToList();

        return stories;
    }
}

public class Story
{
    public string Title { get; set; }
    public string Url { get; set; }
    public int Score { get; set; }
    public string By { get; set; }
    public int Descendants { get; set; }
    [JsonConverter(typeof(UnixEpochMilliSecondConverter))]
    public DateTimeOffset Time { get; set; }

}