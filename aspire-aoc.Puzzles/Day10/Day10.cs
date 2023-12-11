namespace aspire_aoc.Puzzles.Day10;

public static class Day10Extensions 
{
    public static bool IsStart(this (int x, int y) point, List<char[]> maze) => maze[point.y][point.x] == 'S';
    
    public static bool In(this (int x, int y) point, ICollection<(int x, int y)> visited) => visited.Contains(point);
}

public class Day10 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;
    private List<char[]> _maze;

    public Day10()
    {
        _puzzleService = new PuzzleService(this);
        _maze = new List<char[]>();
    }

    /// <summary>
    /*
     * 
        | is a vertical pipe connecting north and south.         ↓ ↑
        - is a horizontal pipe connecting east and west.         → ←
        L is a 90-degree bend connecting north and east.         ↲ ↱
        J is a 90-degree bend connecting north and west.         ↳ ↰
        7 is a 90-degree bend connecting south and west.         ↲ ↰
        F is a 90-degree bend connecting south and east.         ↳ ↰
        . is ground; there is no pipe in this tile.        
        S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
     * 
     */
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public (int P1, int P2) Solve(string input)
    {
        _maze = input.Split(Environment.NewLine).Select(line => line.ToCharArray()).ToList();
        var s = (x : 0, y: 0);
        for (var j = 0; j < _maze.Count; j++)
        {
            for (var i = 0; i < _maze[j].Length; i++)
            {
                if (_maze[j][i] != 'S') continue;
                
                s = (i, j);
                break;
            }
        }
        
        // Now we have S, time to find the circuit
        var circuits =
            new List<(int x, int y)> { (s.x + 1, s.y), (s.x - 1, s.y), (s.x, s.y + 1), (s.x, s.y - 1) }
                .Select(x => Circuit(x, s));
        
        return (circuits.Max(x => x.Count) / 2, 0);
    }
    
    // depth first search for a closed circuit
    private List<(int x, int y)> Circuit((int x, int y) p, (int x, int y) lp)
    {
        var v = new List<(int x, int y)>();
        while (true)
        {
            if (p.x < 0 || p.x > _maze[0].Length || p.y < 0 || p.y > _maze.Count) return [];

            var op = p;
            v.Add(p);
            switch (_maze[p.y][p.x])
            {
                case 'S':
                    // start
                    return v;
                case 'L':
                    // 90-degree bend connecting north and east. ↳ opposite
                    if (!(p.x + 1, p.y).In(v) && (p.x + 1, p.y) != lp) p = (p.x + 1, p.y);
                    else if (!(p.x, p.y - 1).In(v) && (p.x, p.y - 1) != lp) p = (p.x, p.y - 1);
                    else return [];
                    
                    break;
                case 'J':
                    // 90-degree bend connecting north and west. ↲
                    if (!(p.x - 1, p.y).In(v) && (p.x - 1, p.y) != lp) p = (p.x - 1, p.y);
                    else if (!(p.x, p.y - 1).In(v) && (p.x, p.y - 1) != lp) p = (p.x, p.y - 1);
                    else return [];

                    break;
                case '7':
                    // 90-degree bend connecting south and west. ↰
                    if (!(p.x - 1, p.y).In(v) && (p.x - 1, p.y) != lp) p = (p.x - 1, p.y);
                    else if (!(p.x, p.y + 1).In(v) && (p.x, p.y + 1) != lp) p = (p.x, p.y + 1);
                    else return [];
                    
                    break;
                case 'F':
                    // 90-degree bend connecting south and east. ↱
                    if (!(p.x + 1, p.y).In(v) && (p.x + 1, p.y) != lp) p = (p.x + 1, p.y);
                    else if (!(p.x, p.y + 1).In(v) && (p.x, p.y + 1) != lp) p = (p.x, p.y + 1);
                    else return [];

                    break;
                case '|':
                    // vertical pipe connecting north and south. ↓ ↑
                    if (!(p.x, p.y + 1).In(v) && (p.x, p.y + 1) != lp) p = (p.x, p.y + 1);
                    else if (!(p.x, p.y - 1).In(v) && (p.x, p.y - 1) != lp) p = (p.x, p.y - 1);
                    else return [];
                    
                    break;
                case '-':
                    // horizontal pipe connecting east and west. → ←
                    if (!(p.x + 1, p.y).In(v) && (p.x + 1, p.y) != lp) p = (p.x + 1, p.y);
                    else if (!(p.x - 1, p.y).In(v) && (p.x - 1, p.y) != lp) p = (p.x - 1, p.y);
                    else return [];
                    
                    break;
                case '.':
                    // ground; there is no pipe in this tile.
                    return [];
                default:
                    throw new ArgumentOutOfRangeException();
            }

            lp = op;
        }
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