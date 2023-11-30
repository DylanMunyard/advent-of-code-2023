using System.Text;

namespace aspire_aoc.Puzzles.Day3;

public class Day3 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    private (string, string) RuckSack(string input)
    {
        var len = input.Length / 2;
        return (input[..len], input[len..]);
    }

    private int Score(int item)
    {
        if (item is >= 65 and <= 90) return item - 38; /* A-Z */
        
        return item - 96; /* a-z */
    }

    private int Common(string[] inputs)
    {
        var input = inputs[0];
        return input.FirstOrDefault(item => inputs[1..].All(x => x.Contains(item)));
    }

    private int Solve((string, string) ruckSack)
    {
        var (left, right) = ruckSack;
        return Score(Common(new[] { left, right }));
    }
    
    public Day3()
    {
        _puzzleService = new PuzzleService(this);
    }

    public async Task<int> SolvePart1(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample)).Sum(x => Solve(RuckSack(x)));
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        var input = await _puzzleService.PuzzleInput(solveSample);
        var total = 0;
        for (var i = 0; i < input.Length; i += 3)
        {
            total += Score(Common(new[] { input[i], input[i + 1], input[i + 2] }));
        }

        return total;
    }
}