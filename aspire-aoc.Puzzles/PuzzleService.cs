using aspire_aoc.Puzzles.Extensions;

namespace aspire_aoc.Puzzles;

public interface IPuzzleService
{
    public Task<int> SolvePart1(bool solveSample);
    public Task<int> SolvePart2(bool solveSample);
}

public class PuzzleService(IPuzzleService puzzleService)
{
    private readonly Lazy<Task<string[]>> _input = new (puzzleService.InputLines());
    private readonly Lazy<Task<string[]>> _sample = new (puzzleService.SampleInputLines());
    
    public Task<string[]> PuzzleInput(bool solveSample = false) => !solveSample ? _input.Value : _sample.Value;
}