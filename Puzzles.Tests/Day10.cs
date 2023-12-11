using aspire_aoc.Puzzles.Day10;
using FluentAssertions;

namespace Puzzles.Tests;

public class Day10Tests
{
    private readonly Day10 _puzzle = new();

    [Theory]
    [InlineData(true, "8")]
    [InlineData(false, "1798691765")]
    public async Task Part1(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart1(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
    
    [Theory]
    [InlineData(true, "2")]
    [InlineData(false, "1104")]
    public async Task Part2(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}