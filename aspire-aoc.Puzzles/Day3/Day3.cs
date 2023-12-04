using System.Text;
using aspire_aoc.Puzzles;

public class Day3 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day3()
    {
        _puzzleService = new PuzzleService(this);
    }

    private record Point(int X, int Y);

    private (int, int) Solve(IReadOnlyList<string> input)
    {
        var parts = new List<(List<Point> Points, int Number)>();
        var symbols = new List<(Point Point, char Symbol)>();
        for (var y = 0; y < input.Count; y++)
        {
            var sb = new StringBuilder();
            var points = new List<Point>();
            for (var x = 0; x < input[y].Length; x++)
            {
                if (char.IsDigit(input[y][x]))
                {
                    points.Add(new Point(x, y));
                    sb.Append(input[y][x]);
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        parts.Add((points, int.Parse(sb.ToString())));
                    }

                    sb.Clear();
                    points = [];
                    
                    if (input[y][x] != '.')
                    {
                        symbols.Add((new Point(x, y), input[y][x]));
                    }
                } 
            }

            if (sb.Length > 0)
            {
                parts.Add((points, int.Parse(sb.ToString())));
            }
        }
        
        var part1 = parts
            .Where(x => symbols.Any(s => Adjacent(x.Points, s.Point)))
            .Sum(x => x.Number);

        var part2 = symbols
            .Where(x => x.Symbol == '*')
            .Where(x => parts.Count(p => Adjacent(p.Points, x.Point)) == 2)
            .Select(x => parts.Where(p => Adjacent(p.Points, x.Point)).Aggregate(1, (p1, p2) => p1 * p2.Number))
            .Sum();

        return (part1, part2);
    }

    private bool Adjacent(IEnumerable<Point> points, Point point)
    {
        return points.Any(x => Math.Abs(x.X - point.X) <= 1 && Math.Abs(x.Y - point.Y) <= 1);
    }

    public async Task<int> SolvePart1(bool solveSample)
    {
        var (p1, _) = Solve(await _puzzleService.PuzzleInput(solveSample, 1));
        return p1;
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        var (_, p2) = Solve(await _puzzleService.PuzzleInput(solveSample, 2));
        return p2;
    }
}