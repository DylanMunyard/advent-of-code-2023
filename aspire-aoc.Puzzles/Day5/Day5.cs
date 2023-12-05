using System.Collections.Concurrent;

namespace aspire_aoc.Puzzles.Day5;

public class Day5 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day5()
    {
        _puzzleService = new PuzzleService(this);
    }

    private (long Part1, long Part2) Solve(string[] input)
    {
        var seeds = input[0].Substring("seeds: ".Length).Split(new [] { "  ", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        
        var maps = new Dictionary<string, List<(long Source, long Range, long Diff)>>();
        var mappingKey = "";
        foreach (var line in input[2..])
        {
            if (string.IsNullOrEmpty(line)) continue;
            
            if (!line.EndsWith("map:"))
            {
                var range = line.Split(new[] { "  ", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                maps[mappingKey].Add((range[1], range[2], range[0] - range[1]));
            }
            else
            {
                mappingKey = line.Replace(" map:", "");
                maps.TryAdd(mappingKey, []);
            }
        }

        long part1 = long.MaxValue;
        foreach (var seed in seeds)
        {
            var location = seed;
            foreach (var map in maps)
            {
                location += map.Value.FirstOrDefault(x => location >= x.Source && location < x.Source + x.Range).Diff;
            }

            part1 = part1 < location ? part1 : location;
        }

        var seedRanges = new List<(long Source, long Range)>();
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
        });

        return (part1, locations.Min());
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        var (part1, _) = Solve(await _puzzleService.InputAsLines(solveSample, 1));
        return part1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        var (_, part2) = Solve(await _puzzleService.InputAsLines(solveSample, 2));
        return part2.ToString();
    }
}