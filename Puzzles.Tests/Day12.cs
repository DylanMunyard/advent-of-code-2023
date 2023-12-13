using aspire_aoc.Puzzles.Dayx;
using FluentAssertions;

namespace Puzzles.Tests;

public class Day12Tests
{
    private readonly Day12 _puzzle = new();

    [Theory]
    [InlineData(true, "21")]
    [InlineData(false, "7361")]
    public async Task Part1(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart1(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
    
    [Theory]
    [InlineData(true, "525152")]
    [InlineData(false, "84289137")] // 16325541445 is too low
    public async Task Part2(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}