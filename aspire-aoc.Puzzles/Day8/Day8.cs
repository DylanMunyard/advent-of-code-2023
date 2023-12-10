using Serilog.Sinks.File;

namespace aspire_aoc.Puzzles.Day8;

public class Day8 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;
    private Dictionary<string,(string Left, string Right)> _maps;
    private char[] _instructions;

    public Day8()
    {
        _puzzleService = new PuzzleService(this);
    }

    public (long P1, long P2) Solve(string input)
    {
        var parts = input.Split(Environment.NewLine);
        _instructions = parts[0].ToCharArray();
        _maps = new Dictionary<string, (string Left, string Right)>();
        foreach (var part in parts[2..])
        {
            var split = part.Replace("(", "").Replace(")", "").Split(new []{ " = ", ", "}, StringSplitOptions.RemoveEmptyEntries);
            _maps.Add(split[0], (split[1], split[2]));
        }

        var p1 = Steps(Part.One);
        var p2 = Steps(Part.Two);
        
        return (p1, p2);
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P1.ToString();
    }

    private enum Part
    {
        One = 1,
        Two = 2
    }

    private long Steps(Part part)
    {
        var starting = _maps.Where(x => part == Part.One ? x.Key == "AAA" : x.Key.EndsWith("A"))
            .Select(x => x.Key)
            .ToList();
        if (starting.Count == 0) return 0;

        var step = 0;
        var stepCounts = new Dictionary<int, int>();
        while (true)
        {
            var newStarting = new List<string>();

            for (var i = 0; i < starting.Count; i++)
            {
                // find the thing we're looking for, else add to newOrigins and try again
                var point = _instructions[step % _instructions.Length] switch
                {
                    'R' => _maps[starting[i]].Right,
                    'L' => _maps[starting[i]].Left,
                    _ => throw new ArgumentOutOfRangeException()
                };
                if ((part is Part.One && point == "ZZZ") ||
                    (part is Part.Two && point.EndsWith("Z")))
                    stepCounts[i] = step + 1;
                
                newStarting.Add(point);
                
                if (stepCounts.Count == starting.Count)
                {
                    return findlcm(stepCounts.Values.Select(x => long.Parse(x.ToString())).ToList());
                }
            }

            Console.WriteLine($"{string.Join(',', stepCounts.Keys)}: {stepCounts.Values.Sum()}");
            step++;
            starting = newStarting;
        }
    }
    
    static long gcf(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static long findlcm(List<long> steps)
    {
        long answer = 1;
        foreach (var step in steps)
        {
            answer = answer * step / gcf(step, answer);
        }

        return answer;
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 2)).P2.ToString();
    }
}