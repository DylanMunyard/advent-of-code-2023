using aspire_aoc.Puzzles.Extensions;

namespace aspire_aoc.Puzzles;

public interface IPuzzleService
{
    public Task<int> SolvePart1(bool solveSample);
    public Task<int> SolvePart2(bool solveSample);
}

public class PuzzleService(IPuzzleService puzzleService)
{
    private readonly Lazy<Task<string[]>> _input = new(puzzleService.InputLines());

    private Lazy<Task<string[]>> Sample(int part)
    {
        return new Lazy<Task<string[]>>(puzzleService.SampleInputLines(part));
    }

    public Task<string[]> PuzzleInput(bool solveSample, int part) => !solveSample ? _input.Value : Sample(part).Value;
}