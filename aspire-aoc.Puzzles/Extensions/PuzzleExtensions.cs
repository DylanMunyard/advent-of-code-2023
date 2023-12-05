namespace aspire_aoc.Puzzles.Extensions;

public static class PuzzleExtensions
{
    public static Task<string[]> InputLines(this IPuzzleService puzzleService) =>
        File.ReadAllLinesAsync(InputFileName(puzzleService));
    
    public static Task<string> InputString(this IPuzzleService puzzleService) =>
        File.ReadAllTextAsync(InputFileName(puzzleService));

    public static Task<string[]> SampleInputLines(this IPuzzleService puzzleService, int part) =>
        File.ReadAllLinesAsync(SampleInputFileName(puzzleService, part));

    public static Task<string> SampleInputString(this IPuzzleService puzzleService, int part) =>
        File.ReadAllTextAsync(SampleInputFileName(puzzleService, part));

    private static string SampleInputFileName(IPuzzleService puzzleService, int part)
    {
        var day = int.Parse(puzzleService.GetType().Name.Replace("Day", string.Empty));
        return Path.Combine(
            $"Day{day}",
            File.Exists(Path.Combine($"Day{day}", $"sample-part{part}.txt"))
                ? $"sample-part{part}.txt"
                : "sample.txt");
    }

    private static string InputFileName(IPuzzleService puzzleService)
    {
        var day = int.Parse(puzzleService.GetType().Name.Replace("Day", string.Empty));
        return Path.Combine($"Day{day}", "input.txt");
    }
}