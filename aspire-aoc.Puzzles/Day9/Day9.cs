using System.Collections.Immutable;

namespace aspire_aoc.Puzzles.Day9;

public class Day9 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day9()
    {
        _puzzleService = new PuzzleService(this);
    }

    private class Sequence(string input)
    {
        private readonly List<long> _numbers = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

        public long Predict(bool part1)
        {
            return Reduce(_numbers, part1);
        }

        private long Reduce(List<long> numbers, bool part1)
        {
            if (numbers.All(x => x == 0))
                return 0;
    
            var nl = numbers.Zip(numbers.Skip(1), (current, next) => next - current).ToList();
            var result = Reduce(nl, part1);

            if (part1)
            {
                return numbers[^1] + result;
            }

            return numbers[0] - result;
        }
    }

    public (long P1, long P2) Solve(string[] sequences)
    {
        var p1 = sequences.Select(input => new Sequence(input)).Select(sequence => sequence.Predict(true)).Sum();
        var p2 = sequences.Select(input => new Sequence(input)).Select(sequence => sequence.Predict(false)).Sum();

        return (p1, p2);
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsLines(solveSample, 1)).P1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsLines(solveSample, 1)).P2.ToString();
    }
}