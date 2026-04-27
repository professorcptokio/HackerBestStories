using HackerBestStories.API.Services;
using HackerBestStories.API.Services.Impementation;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
    configure.AddDebug();
});

builder.Services.ConfigureHttpClientDefaults(http =>
    http.ConfigureHttpClient(client => client.Timeout = TimeSpan.FromSeconds(30)));

builder.Services.AddScoped<IHackerNewsExternalService>(sp =>
    new HackerNewsExternalService(
        sp.GetRequiredService<IHttpClientFactory>().CreateClient(),
        builder.Configuration,
        sp.GetRequiredService<ILogger<HackerNewsExternalService>>()));

var redisConnection = builder.Configuration["REDIS_CONNECTION"] ?? "localhost:6379";
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(redisConnection));

builder.Services.AddScoped<ICacheService, RedisCacheService>();

builder.Services.AddScoped<IStoryService, StoryService>();


var app = builder.Build();


app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
