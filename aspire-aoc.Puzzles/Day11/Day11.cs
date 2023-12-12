namespace aspire_aoc.Puzzles.Day11;

public class Day11 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;
    private List<string> _grid;
    private HashSet<int> _re; // row expansions
    private HashSet<int> _ce; // column expansions

    public Day11()
    {
        _puzzleService = new PuzzleService(this);
    }

    public (long P1, long P2) Solve(string input)
    {
        _re = [];
        _ce = [];
        _grid = [];
        var galaxies = new List<(int r, int c)>();
        var rows = input.Split(Environment.NewLine);
        for (var r = 0; r < rows.Length; r++)
        {
            var row = rows[r]; 
            _grid.Add(row);
            // Expand rows that are all '.'
            if (row.ToCharArray().All(x => x == '.'))
            {
                _re.Add(r);
            }
        }

        // Find columns that are all '.'
        for (var c = 0; c < _grid[0].Length; c++)
        {
            if (_grid.All(x => x[c] == '.'))
                _ce.Add(c);
        }
        
        // Find galaxies
        for (var r = 0; r < _grid.Count; r++)
        {
            for (var c = 0; c < _grid[r].Length; c++)
            {
                if (_grid[r][c] == '#')
                {
                    galaxies.Add((r, c));
                }
            }
        }

        var galaxyMap = new List<(int r1, int c1, int r2, int c2)>();
        for (var g = 0; g < galaxies.Count; g++)
        {
            for (var h = g + 1; h < galaxies.Count; h++)
            {
                var (r1, c1) = galaxies[g];
                var (r2, c2) = galaxies[h];
                galaxyMap.Add((r1, c1, r2, c2));
            }
        }

        var p1 = galaxyMap.Sum(x => MinDistance(x, 2));
        var p2 = galaxyMap.Sum(x => MinDistance(x, 1_000_000));

        return (p1, p2);
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P2.ToString();
    }
    
    private class Node(int r, int c, long d)
    {
        public readonly int R = r;
        public readonly int C = c;
        public readonly long Distance = d;
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private long Distance(Node n, (int r, int c) cell, Direction direction, int expanse)
    {
       // Distance between cells is 1, except if travelling through an expanse, then the distance is 1 * expanse 
        switch (direction)
        {
            case Direction.Up:
            case Direction.Down:
            {
                if (_re.Contains(cell.r))
                {
                    return n.Distance + 1 * expanse;
                }

                break;
            }
            case Direction.Left:
            case Direction.Right:
            {
                if (_ce.Contains(cell.c))
                {
                    return n.Distance + 1 * expanse;
                }

                break;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        return n.Distance + 1;
    }
    
    /// <summary>
    /// (algorithm from https://www.geeksforgeeks.org/shortest-distance-two-cells-matrix-grid/)
    /// From the starting cell (sr, sc) find the shortest path to ending cell (er, ec)
    /// </summary>
    /// <param name="galaxy"></param>
    /// <param name="e">The scale of the voids</param>
    /// <returns></returns>
    private long MinDistance((int sr, int sc, int er, int ec) galaxy, int e)
    {
        var source = new Node(galaxy.sr, galaxy.sc, 0);
 
        // To keep track of visited QItems. Marking
        // blocked cells as visited.
        var visited = new bool[_grid.Count, _grid[0].Length];
 
        // applying BFS on matrix cells starting from source
        var q = new Queue<Node>();
        q.Enqueue(source);
        visited[source.R, source.C] = true;
        while (q.Count > 0)
        {
            var p = q.Peek();
            q.Dequeue();

            // Destination found;
            if ((p.R, p.C) == (galaxy.er, galaxy.ec))
            {
                return p.Distance;
            }

            // moving up
            if (p.R - 1 >= 0 && visited[p.R - 1, p.C] == false)
            {
                q.Enqueue(new Node(p.R - 1, p.C, Distance(p, (p.R - 1, p.C), Direction.Up, e)));
                visited[p.R - 1, p.C] = true;
            }

            // moving down
            if (p.R + 1 < _grid.Count && visited[p.R + 1, p.C] == false)
            {
                q.Enqueue(new Node(p.R + 1, p.C, Distance(p, (p.R + 1, p.C), Direction.Down, e)));
                visited[p.R + 1, p.C] = true;
            }

            // moving left
            if (p.C - 1 >= 0 && visited[p.R, p.C - 1] == false)
            {
                q.Enqueue(new Node(p.R, p.C - 1, Distance(p, (p.R, p.C - 1), Direction.Left, e)));
                visited[p.R, p.C - 1] = true;
            }

            // moving right
            if (p.C + 1 < _grid[0].Length && visited[p.R, p.C + 1] == false)
            {
                q.Enqueue(new Node(p.R, p.C + 1, Distance(p, (p.R, p.C + 1), Direction.Right, e)));
                visited[p.R, p.C + 1] = true;
            }
        }

        return 0;
    }
}