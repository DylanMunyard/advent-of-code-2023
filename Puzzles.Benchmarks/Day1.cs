using aspire_aoc.Puzzles;
using aspire_aoc.Puzzles.Day1;
using BenchmarkDotNet.Attributes;

namespace Puzzles.Benchmarks;

[SimpleJob]
[MemoryDiagnoser]
public class Day1Tests
{
    private IPuzzleService _puzzle = null!;
    
    [GlobalSetup]
    public void Setup()
    {
        _puzzle = new Day1();
    }

    [Benchmark]
    public Task<int> Part1Benchmark() => _puzzle.SolvePart1(false);

    [Benchmark]
    public Task<int> Part2Benchmark() => _puzzle.SolvePart1(false);
}