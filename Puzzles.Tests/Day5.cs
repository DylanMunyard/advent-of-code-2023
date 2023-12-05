using aspire_aoc.Puzzles.Day5;
using FluentAssertions;

namespace Puzzles.Tests;

public class Day5Tests
{
    private readonly Day5 _puzzle = new();

    [Theory]
    [InlineData(true, "35")]
    [InlineData(false, "318728750")]
    public async Task Part1(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart1(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
    
    [Theory]
    [InlineData(true, "46")]
    [InlineData(false, "37384986")]
    public async Task Part2(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}