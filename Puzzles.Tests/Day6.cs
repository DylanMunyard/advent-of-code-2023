using aspire_aoc.Puzzles.Day6;
using FluentAssertions;

namespace Puzzles.Tests;

public class Day6Tests
{
    private readonly Day6 _puzzle = new();

    [Theory]
    [InlineData(true, "288")]
    [InlineData(false, "2344708")]
    public async Task Part1(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart1(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
    
    [Theory]
    [InlineData(true, "71503")]
    [InlineData(false, "30125202")]
    public async Task Part2(bool solveSample, string answer)
    {
        
        var lh = Math.Floor((7 + Math.Sqrt(Math.Pow(7, 2) - 4 * 9)) / 2);
        var th = Math.Ceiling((7 - Math.Sqrt(Math.Pow(7, 2) - 4 * 9)) / 2);
        
        
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}