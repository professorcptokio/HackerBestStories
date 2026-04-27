using HackerBestStories.API.Services;
using HackerBestStories.API.Services.Impementation;
using Scalar.AspNetCore;

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

builder.Services.AddScoped<IStoryService, StoryService>();


var app = builder.Build();


app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
