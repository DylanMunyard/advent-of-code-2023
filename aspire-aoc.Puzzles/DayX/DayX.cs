namespace aspire_aoc.Puzzles.Dayx;

public class DayX : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public DayX()
    {
        _puzzleService = new PuzzleService(this);
    }

    public (int P1, int P2) Solve(string input)
    {
        return (0, 0);
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 2)).P2.ToString();
    }
}