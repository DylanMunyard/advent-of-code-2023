using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using ILogger = Serilog.ILogger;

namespace aspire_aoc.Puzzles.Api;

public class Api
{
    private readonly IDistributedCache _cache;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public Api(IDistributedCache cache, IServiceProvider serviceProvider)
    {
        _cache = cache;
        _serviceProvider = serviceProvider;
        _logger = Log.ForContext<Api>();
    }
    
    public async Task<string> Handler(int day, int part)
    {
        _logger.Information("Solving day {Day} part {Part}", day, part);

        var cacheKey = $"{day}/{part}";
        var cached = await _cache.GetAsync(cacheKey);
        if (cached?.Length > 0)
        {
            _logger.Information("Puzzle cache hit! {PuzzleKey}", cacheKey);
            return System.Text.Encoding.UTF8.GetString(cached, 0, cached.Length);
        }
        
        IPuzzleService puzzleService;
        try
        {
            puzzleService = _serviceProvider.GetRequiredKeyedService<IPuzzleService>(day);
        }
        catch (InvalidOperationException ex)
        {
            _logger.Error(ex, "There's no puzzle service for day {Day}", day);
            return "0";
        }

        try
        {
            var solution = part switch
            {
                1 => await puzzleService.SolvePart1(false),
                2 => await puzzleService.SolvePart2(false),
                _ => throw new ArgumentOutOfRangeException(nameof(part), part, "What part am I solving? 1 or 2?")
            };
            await _cache.SetAsync(cacheKey, System.Text.Encoding.UTF8.GetBytes(solution));
            return solution;
        }
        catch (NotImplementedException ex)
        {
            _logger.Error(ex, "lazy! day {Day} part {Part} not implemented", day, part);
            return "0";
        }
    }
}