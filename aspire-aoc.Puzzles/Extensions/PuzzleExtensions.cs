namespace aspire_aoc.Puzzles.Extensions;

public static class PuzzleExtensions
{
    public static Task<string[]> InputLines(this IPuzzleService puzzleService)
    {
        return File.ReadAllLinesAsync(Path.Combine($"Day{puzzleService.Day}", "input.txt"));
    }
}