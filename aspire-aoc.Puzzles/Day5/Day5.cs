using System.Collections.Concurrent;

namespace aspire_aoc.Puzzles.Day5;

public class Day5 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day5()
    {
        _puzzleService = new PuzzleService(this);
    }

    private class Map
    {
        private List<(long Dest, long Source, long Range)> Tuples { get; } = [];

        public Map(string maps)
        {
            Tuples.AddRange(maps.Split('\n')[1..].Select(x => x.Split())
                .Select(x => (long.Parse(x[0]), long.Parse(x[1]), long.Parse(x[2]))));
        }

        public long ApplyOne(long seed)
        {
            foreach (var tuple in Tuples)
            {
                if (seed >= tuple.Source && seed < tuple.Source + tuple.Range)
                    return seed + tuple.Dest - tuple.Source;
            }

            return seed;
        }
    }

    private (long Part1, long Part2) Solve(string input)
    {
        var parts = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var seeds = parts[0]["seeds: ".Length..].Split(new [] { "  ", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

        var mappings = parts[1..].Select(x => new Map(x)).ToList();
        var p1 = seeds.Select(seed => mappings.Aggregate(seed, (current, map) => map.ApplyOne(current))).ToList();

        /*var seedRanges = new List<(long Source, long Range)>();
        for (var i = 0; i < seeds.Length; i += 2)
        {
            seedRanges.Add((seeds[i], seeds[i] + seeds[i + 1]));
        }

        var locations = new ConcurrentBag<long>();
        Parallel.ForEach(seedRanges, seedRange =>
        {
            var min = long.MaxValue;
            for (var seed = seedRange.Source; seed < seedRange.Range; seed++)
            {
                var location = seed;
                foreach (var map in maps)
                {
                    var location1 = location;
                    location += map.Value.FirstOrDefault(x => location1 >= x.Source && location1 < x.Source + x.Range).Diff;;
                }

                min = min < location ? min : location;
            }

            locations.Add(min);
        });*/

        return (p1.Min(), 0);
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        var (part1, _) = Solve(await _puzzleService.InputAsString(solveSample, 1));
        return part1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        var (_, part2) = Solve(await _puzzleService.InputAsString(solveSample, 2));
        return part2.ToString();
    }
}