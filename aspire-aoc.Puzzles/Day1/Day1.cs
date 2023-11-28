namespace aspire_aoc.Puzzles.Day1;

public class Day1 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day1()
    {
        _puzzleService = new PuzzleService(this);
    }
    
    private async Task<List<int>> Solve(bool solveSample)
    {
        var calories = new List<int>();

        var total = 0;
        foreach (var calorie in await _puzzleService.PuzzleInput(solveSample))
        {
            if (string.IsNullOrEmpty(calorie))
            {
                calories.Add(total);
                total = 0;
                continue;
            }

            total += int.Parse(calorie);
        }
        calories.Add(total); // last line

        return calories;
    }

    public async Task<int> SolvePart1(bool solveSample)
    {
        var elves = await Solve(solveSample);
        return elves.Max();
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        var elves = await Solve(solveSample);
        return elves.OrderDescending().Take(3).Sum();
    }
}