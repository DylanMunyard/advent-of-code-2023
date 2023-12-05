using aspire_aoc.Puzzles.Extensions;

namespace aspire_aoc.Puzzles;

public interface IPuzzleService
{
    public Task<string> SolvePart1(bool solveSample);
    public Task<string> SolvePart2(bool solveSample);
}

public class PuzzleService(IPuzzleService puzzleService)
{
    private readonly Lazy<Task<string[]>> _inputLines = new(puzzleService.InputLines());
    private readonly Lazy<Task<string>> _inputString = new(puzzleService.InputString());

    private Lazy<Task<string[]>> SampleLines(int part) => new (puzzleService.SampleInputLines(part));
    private Lazy<Task<string>> SampleString(int part) => new (puzzleService.SampleInputString(part));

    public Task<string[]> InputAsLines(bool solveSample, int part) => !solveSample ? _inputLines.Value : SampleLines(part).Value;

    public Task<string> InputAsString(bool solveSample, int part) => !solveSample ? _inputString.Value : SampleString(part).Value;
}