namespace aspire_aoc.Puzzles.Day6;

public class Day6 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day6()
    {
        _puzzleService = new PuzzleService(this);
    }

    public (int P1, int P2) Solve(string input)
    {
        var parts = input.Split(Environment.NewLine);
        var (times, distances) = (
            parts[0].Split(": ")[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(),
            parts[1].Split(": ")[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());

        var p1 = times.Zip(distances)
            .Select(x => Enumerable.Range(0, x.First).Count(t => t * (x.First - t) > x.Second))
            .Aggregate((t1, t2) => t1 * t2);

        // P2 by solving quadratic equation
        // B²-T*B+D=0, where B = button pressed, T = Max Time, D = Distance
        // Solving the equation will give us the times that will be equal to the distance.
        // So we want all the times between the two solutions, inclusive (+ 1)
        var p2Time = long.Parse(string.Join("", times.Select(x => x.ToString())));
        var p2Distance = long.Parse(string.Join("", distances.Select(x => x.ToString())));
        var maxTime = Math.Floor((p2Time + Math.Sqrt(Math.Pow(p2Time, 2) - 4 * p2Distance)) / 2); // any time higher will be less than the distance
        var minTime = Math.Ceiling((p2Time - Math.Sqrt(Math.Pow(p2Time, 2) - 4 * p2Distance)) / 2); // any time lower will be less than the distance

        return (p1, (int)(maxTime - minTime + 1));
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P2.ToString();
    }
}