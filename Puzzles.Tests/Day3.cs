using FluentAssertions;

namespace Puzzles.Tests;

public class Day3Tests
{
    private readonly Day3 _puzzle = new();

    [Theory]
    [InlineData(true, "4361")]
    [InlineData(false, "525181")]
    public async Task Part1(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart1(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
    
    [Theory]
    [InlineData(true, "467835")]
    [InlineData(false, "84289137")]
    public async Task Part2(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}