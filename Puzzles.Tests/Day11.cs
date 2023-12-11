using aspire_aoc.Puzzles.Day11;
using FluentAssertions;

namespace Puzzles.Tests;

public class Day11Tests
{
    private readonly Day11 _puzzle = new();

    [Theory]
    [InlineData(true, "374")]
    [InlineData(false, "9686930")]
    public async Task Part1(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart1(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
    
    [Theory]
    [InlineData(true, "30")]
    [InlineData(false, "84289137")]
    public async Task Part2(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}