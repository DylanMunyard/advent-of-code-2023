namespace aspire_aoc.Puzzles.Dayx;

public class Day12 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day12()
    {
        _puzzleService = new PuzzleService(this);
    }

    private List<List<(int s, int e)>> recursive(int i, int[] R, string C)
    {
        if (R.Length == 0 || i >= C.Length) return [];

        var result = new List<List<(int s, int e)>>();

        var start = i;
        while (i + R[0] <= C.Length)
        {
            if (!is_match(start, i, C, R[0]))
            {
                i++;
                continue;
            }

            var nc = new List<(int s, int e)> { (i, i + R[0]) };
            var combinations = recursive(i + R[0] + 1, R.Skip(1).ToArray(), C);
            if (combinations.Count == 0)
            {
                result.Add(nc);
            }
            else
            {
                foreach (var c in combinations)
                {
                    var r = new List<(int s, int e)>(nc);
                    r.AddRange(c);
                    result.Add(r);
                }
            }

            i++;
        }

        return result;
    }

    public (int P1, int P2) Solve(string[] input)
    {
        var p1 = 0;
        foreach (var line in input)
        {
            var row = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var C = row[0];
            var R = row[1].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            var results = recursive(0, R, C).Where(x => x.Count == R.Length);
            p1 += results.Count();
        }
        
        return (p1, 0);
    }

    bool is_match(int s, int i, string C, int R)
    {
        try
        {
            var b = C.Substring(s, i - s); // chars prior to match
            var m = C.Substring(i, R);
            // check all the chars from start are not '#'

            return b.ToCharArray().All(x => x != '#') &&
                   m.ToCharArray().All(x => x is '#' or '?') &&
                   // check the next character is (or can be) a gap, or the end of the line
                   (i + R >= C.Length || C[i + R] is '.' or '?');
        }
        catch (ArgumentOutOfRangeException ex)
        {
            var ting = ex;
            
        }

        return false;
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsLines(solveSample, 1)).P1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsLines(solveSample, 2)).P2.ToString();
    }
}