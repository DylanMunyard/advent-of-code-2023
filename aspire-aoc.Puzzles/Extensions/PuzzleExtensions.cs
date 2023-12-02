namespace aspire_aoc.Puzzles.Extensions;

public static class PuzzleExtensions
{
    public static Task<string[]> InputLines(this IPuzzleService puzzleService) =>
        FileLines(puzzleService, "input.txt");

    public static Task<string[]> SampleInputLines(this IPuzzleService puzzleService, int part)
    {
        if (!File.Exists($"sample-part{part}.txt"))
        {
            return FileLines(puzzleService, $"sample-part{part}.txt");
        }
        return FileLines(puzzleService, "sample.txt");
    }

    private static Task<string[]> FileLines(IPuzzleService puzzleService, string fileName)
    {
        var day = int.Parse(puzzleService.GetType().Name.Replace("Day", string.Empty));
        return File.ReadAllLinesAsync(Path.Combine($"Day{day}", fileName));
    }
}