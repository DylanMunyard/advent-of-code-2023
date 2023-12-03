namespace aspire_aoc.Puzzles.Extensions;

public static class PuzzleExtensions
{
    public static Task<string[]> InputLines(this IPuzzleService puzzleService) =>
        InputFileLines(puzzleService);

    public static Task<string[]> SampleInputLines(this IPuzzleService puzzleService, int part)
    {
        var day = int.Parse(puzzleService.GetType().Name.Replace("Day", string.Empty));
        if (File.Exists(Path.Combine($"Day{day}", $"sample-part{part}.txt")))
        {
            return FileLines(day, $"sample-part{part}.txt");
        }
        return FileLines(day, "sample.txt");
    }

    private static Task<string[]> InputFileLines(IPuzzleService puzzleService)
    {
        var day = int.Parse(puzzleService.GetType().Name.Replace("Day", string.Empty));
        return FileLines(day, "input.txt");
    }

    private static Task<string[]> FileLines(int day, string fileName)
    {
        return File.ReadAllLinesAsync(Path.Combine($"Day{day}", fileName));
    }
}