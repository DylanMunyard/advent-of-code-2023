using aspire_aoc.Puzzles.Api;
using aspire_aoc.Puzzles.Day1;
using aspire_aoc.Puzzles.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var log = new LoggerConfiguration()
    .WriteTo.Console();

if (builder.Configuration.GetConnectionString("seq") is string connectionString)
{
    log.WriteTo.Seq($"http://{connectionString}");
}

Log.Logger = log.CreateLogger();

Log.Information("Puzzle API started");
Log.Information("Seq URL {SeqServer}", builder.Configuration.GetConnectionString("seq"));


// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.AddRedisDistributedCache("cache");
builder.Services.AddProblemDetails();
builder.Services.AddTransient<Api>();
builder.Services.AddPuzzle<Day1>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet("/solve/{Day}/part/{Part}", async (int day, int part) =>
{
    var api = app.Services.GetService<Api>();
    return await api!.Handler(day, part);
});

app.MapDefaultEndpoints();

app.Run();