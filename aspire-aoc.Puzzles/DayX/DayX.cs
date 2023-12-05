namespace aspire_aoc.Puzzles.Dayx;

public class DayX : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public DayX()
    {
        _puzzleService = new PuzzleService(this);
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return (await _puzzleService.InputAsLines(solveSample, 1)).Length.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return (await _puzzleService.InputAsLines(solveSample, 2)).Length.ToString();
    }
}