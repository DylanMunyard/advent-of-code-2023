using System.Runtime.CompilerServices;
using aspire_aoc.Puzzles.Extensions;

namespace aspire_aoc.Puzzles.Day1;

public class Day1 : IPuzzleService
{
    public int Day => 1;

    private async Task<List<int>> Elves()
    {
        var calories = new List<int>();

        var total = 0;
        foreach (var calorie in await this.InputLines())
        {
            if (string.IsNullOrEmpty(calorie))
            {
                calories.Add(total);
                total = 0;
                continue;
            }

            total += int.Parse(calorie);
        }

        return calories;
    }

    public async Task<int> SolvePart1()
    {
        var elves = await Elves();
        return elves.Max();
    }

    public async Task<int> SolvePart2()
    {
        var elves = await Elves();
        return elves.OrderDescending().Take(3).Sum();
    }
}