using aspire_aoc.Puzzles;
using aspire_aoc.Puzzles.Day3;
using BenchmarkDotNet.Attributes;

namespace Puzzles.Benchmarks;

[SimpleJob]
[MemoryDiagnoser]
public class Day3Tests
{
    private IPuzzleService _puzzle = null!;
    
    [GlobalSetup]
    public void Setup()
    {
        _puzzle = new Day3();
    }

    [Benchmark]
    public Task<int> Part1Benchmark() => _puzzle.SolvePart1(false);

    [Benchmark]
    public Task<int> Part2Benchmark() => _puzzle.SolvePart1(false);
}