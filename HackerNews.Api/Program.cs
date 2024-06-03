using HackerNews.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default30",
        new CacheProfile()
        {
            Duration = 30
        });
});

builder.Services.AddHttpClient<IHackerNewsService, HackerNewsService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["HackerNewsApi"]);
}).AddPolicyHandler(GetRetryPolicy())
.AddPolicyHandler(GetRateLimitPolicy());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.MapControllers();

app.Run();




static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{

    var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);

    var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
         .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
         .WaitAndRetryAsync(delay);

    return retryPolicy;
}

static IAsyncPolicy<HttpResponseMessage> GetRateLimitPolicy()
{
    return Policy.BulkheadAsync<HttpResponseMessage>(5, 10);
    
}

