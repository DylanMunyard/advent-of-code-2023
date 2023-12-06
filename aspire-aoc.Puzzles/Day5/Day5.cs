using Range = (long Start, long End);

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
        private List<(long Dest, long Start, long Range)> Tuples { get; } = [];

        public Map(string maps)
        {
            Tuples.AddRange(maps.Split('\n')[1..].Select(x => x.Split())
                .Select(x => (long.Parse(x[0]), long.Parse(x[1]), long.Parse(x[2]))));
        }

        public long ApplyOne(long seed)
        {
            foreach (var tuple in Tuples)
            {
                if (seed >= tuple.Start && seed < tuple.Start + tuple.Range)
                    return seed - tuple.Start + tuple.Dest ;
            }

            return seed;
        }

        /// <summary>
        /// This clever mofo explained how this works https://www.youtube.com/watch?v=iqTopXV13LE
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns></returns>
        public List<Range> ApplyRange(List<Range> ranges)
        {
            var answerRanges = new List<Range>();
            foreach (var (dest, source, size) in Tuples)
            {
                var sourceEnd = source + size;
                var newRanges = new List<Range>();
                while (ranges.Count > 0)
                {
                    var (start, end) = ranges[^1];
                    ranges.RemoveAt(ranges.Count - 1);
                    var before = new Range(start, Math.Min(end, source));
                    var inter = new Range(Math.Max(start, source), Math.Min(sourceEnd, end));
                    var after = new Range(Math.Max(sourceEnd, start), end);

                    if (before.End > before.Start)
                    {
                        newRanges.Add(before);
                    }

                    if (after.End > after.Start)
                    {
                        newRanges.Add(after);
                    }

                    if (inter.End > inter.Start)
                    {
                        answerRanges.Add((inter.Start - source + dest, inter.End - source + dest));
                    }
                }

                ranges = newRanges;
            }
            answerRanges.AddRange(ranges);
            return answerRanges;
        }
    }

    private (long Part1, long Part2) Solve(string input)
    {
        var parts = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var seeds = parts[0]["seeds: ".Length..].Split(new [] { "  ", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

        var mappings = parts[1..].Select(x => new Map(x)).ToList();
        var p1 = seeds.Select(seed => mappings.Aggregate(seed, (current, map) => map.ApplyOne(current))).ToList();

        var seedRanges = seeds // tuples of seeds paired up by odd and even indexes
            .Where((s, i) => i % 2 == 0)
            .Zip(seeds.Where((s, i) => i % 2 == 1))
            .Select(x => new Range(x.First, x.Second));

        var p2 = new List<long>();
        foreach (var (start, size) in seedRanges)
        {
            var r = new List<Range> { (start, start + size) };
            r = mappings.Aggregate(r, (current, map) => map.ApplyRange(current));
            p2.Add(r.Min().Start);
        }
        
        // For shame: brute force left in for posterity
        /*var seedRanges = new List<(long Start, long Range)>();
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

        return (p1.Min(), p2.Min());
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