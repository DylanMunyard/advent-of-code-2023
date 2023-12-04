namespace aspire_aoc.Puzzles.Dayx;

public class DayX : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public DayX()
    {
        _puzzleService = new PuzzleService(this);
    }
    
    

    public async Task<int> SolvePart1(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample, 1)).Length;
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample, 2)).Length;
    }
}