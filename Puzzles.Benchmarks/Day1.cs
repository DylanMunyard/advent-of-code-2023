using aspire_aoc.Puzzles;
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
    public Task<string> Part1Benchmark() => _puzzle.SolvePart1(false);

    [Benchmark]
    public Task<string> Part2Benchmark() => _puzzle.SolvePart1(false);
}