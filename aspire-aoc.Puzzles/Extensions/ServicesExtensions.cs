namespace aspire_aoc.Puzzles.Extensions;

public static class ServicesExtensions
{
    public static void  AddPuzzle<TPuzzle>(this IServiceCollection services)
        where TPuzzle : class, IPuzzleService
    {
        var day = int.Parse(typeof(TPuzzle).Name.Replace("Day", string.Empty));
        services.AddKeyedSingleton<IPuzzleService, TPuzzle>(day);
    }
}