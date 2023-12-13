using System.Text;

namespace aspire_aoc.Puzzles.Dayx;

public class Day12 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day12()
    {
        _puzzleService = new PuzzleService(this);
    }

    // could not solve P2, found solution here: https://www.youtube.com/watch?v=xTGkP2GNmbQ
    private Dictionary<(int i, int bi, int current), long> _cache = [];
    private long f(string dots, IList<int> blocks, int i, int bi, int current)
    {
        var key = (i, bi, current);
        if (_cache.TryGetValue(key, out var cached)) return cached;
        
        if (i == dots.Length)
        {
            if (bi == blocks.Count && current == 0)
                return 1;
            if (bi == blocks.Count - 1 && current == blocks[bi])
                return 1;
            return 0;
        }

        long ans = 0;
        foreach (var c in new [] {'.', '#'})
        {
            if (dots[i] == c || dots[i] == '?')
            {
                if (c == '.' && current == 0)
                    ans += f(dots, blocks, i + 1, bi, 0);
                else if (c == '.' && current > 0 && bi < blocks.Count && blocks[bi] == current)
                    ans += f(dots, blocks, i + 1, bi + 1, 0);
                else if (c == '#')
                    ans += f(dots, blocks, i + 1, bi, current + 1);
            }
        }

        _cache[key] = ans;

        return ans;
    }

    public (long P1, long P2) Solve(string[] input, bool part1)
    {
        long p1 = 0;
        foreach (var line in input/* new [] { ".??..??...?##. 1,1,3" }*/)
        {
            var row = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var C = row[0];
            var R = row[1];
            
            if (!part1)
            {
                C = string.Join("?", [C, C, C, C, C]);
                R = string.Join(",", [R, R, R, R, R]);
            }

            var blocks = R.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            _cache.Clear();
            p1 += f(C, blocks, 0, 0, 0);
        }
        
        return (p1, p1);
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsLines(solveSample, 1), true).P1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsLines(solveSample, 2), false).P2.ToString();
    }
}