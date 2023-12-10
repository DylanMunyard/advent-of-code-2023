using aspire_aoc.Puzzles.Day8;
using FluentAssertions;

namespace Puzzles.Tests;

public class Day8Tests
{
    private readonly Day8 _puzzle = new();

    [Theory]
    [InlineData(true, "2")]
    [InlineData(false, "18023")]
    public async Task Part1(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart1(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
    
    [Theory]
    [InlineData(true, "6")]
    [InlineData(false, "14449445933179")] // 448_212_188 is too low
    public async Task Part2(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}