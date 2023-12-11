namespace aspire_aoc.Puzzles.Day11;

public class Day11 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;
    private List<string> _grid;

    public Day11()
    {
        _puzzleService = new PuzzleService(this);
    }

    public (int P1, int P2) Solve(string input)
    {
        _grid = new List<string>();
        var galaxies = new List<(int r, int c)>();
        foreach (var row in input.Split(Environment.NewLine))
        {
            // Expand rows that are all '.'
            if (row.ToCharArray().All(x => x == '.'))
            {
                _grid.AddRange([row, row]);
            }
            else
            {
                _grid.Add(row);
            }
        }

        // Find columns that are all '.'
        var expandCols = new List<int>();
        for (var c = _grid[0].Length - 1; c >= 0; c--)
        {
            if (_grid.All(x => x[c] == '.'))
                expandCols.Add(c);
        }

        // Expand columns that are all '.'
        for (var r = 0; r < _grid.Count; r++)
        {
            var nr = _grid[r];
            for (var c = nr.Length - 1; c >= 0; c--)
            {
                if (expandCols.Contains(c))
                {
                    nr = nr.Insert(c, ".");
                }
            }

            _grid[r] = nr;
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

        var temp = MinDistance((0, 4, 1, 9));
        var p1 = galaxyMap.Sum(MinDistance);

        return (p1, 0);
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P2.ToString();
    }
    
    private class Node(int r, int c, int d)
    {
        public readonly int R = r;
        public readonly int C = c;
        public readonly int Distance = d;
    }
    
    private int MinDistance((int sr, int sc, int er, int ec) galaxy)
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
                q.Enqueue(new Node(p.R - 1, p.C, p.Distance + 1));
                visited[p.R - 1, p.C] = true;
            }

            // moving down
            if (p.R + 1 < _grid.Count && visited[p.R + 1, p.C] == false)
            {
                q.Enqueue(new Node(p.R + 1, p.C, p.Distance + 1));
                visited[p.R + 1, p.C] = true;
            }

            // moving left
            if (p.C - 1 >= 0 && visited[p.R, p.C - 1] == false)
            {
                q.Enqueue(new Node(p.R, p.C - 1, p.Distance + 1));
                visited[p.R, p.C - 1] = true;
            }

            // moving right
            if (p.C + 1 < _grid[0].Length && visited[p.R, p.C + 1] == false)
            {
                q.Enqueue(new Node(p.R, p.C + 1, p.Distance + 1));
                visited[p.R, p.C + 1] = true;
            }
        }

        throw new ArgumentOutOfRangeException();
        return -1;
    }
}