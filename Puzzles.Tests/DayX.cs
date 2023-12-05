using aspire_aoc.Puzzles.Dayx;
using FluentAssertions;

namespace Puzzles.Tests;

public class DayXTests
{
    private readonly DayX _puzzle = new();

    [Theory]
    [InlineData(true, "13")]
    [InlineData(false, "25004")]
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