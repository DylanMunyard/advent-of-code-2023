using aspire_aoc.Puzzles;
using aspire_aoc.Puzzles.Day1;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddKeyedSingleton<IPuzzleService, Day1>(1);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet("/solve/{Day}/part/{Part}", async (int day, int part) =>
{
    IPuzzleService puzzleService;
    try
    {
        puzzleService = app.Services.GetRequiredKeyedService<IPuzzleService>(day);
    }
    catch (InvalidOperationException ex)
    {
        // todo logging
        Console.WriteLine(ex.Message);
        return 0;
    }

    try
    {
        return part switch
        {
            1 => await puzzleService.SolvePart1(),
            2 => await puzzleService.SolvePart2(),
            _ => throw new ArgumentOutOfRangeException(nameof(part), part, "What part am I solving? 1 or 2?")
        };
    }
    catch (NotImplementedException ex)
    {
        // todo logging
        Console.WriteLine("lazy! day {0} part {1} not implemented", day, part);
        return 0;
    }
});

app.MapDefaultEndpoints();

app.Run();