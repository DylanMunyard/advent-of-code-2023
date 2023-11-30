using aspire_aoc.Puzzles.Day3;
using FluentAssertions;

namespace Puzzles.Tests;

public class Day3Tests
{
    private readonly Day3 _puzzle = new();

    [Theory]
    [InlineData(true, 157)]
    [InlineData(false, 7821)]
    public async Task Part1(bool solveSample, int answer)
    {
        // Act
        var solution = await _puzzle.SolvePart1(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
    
    [Theory]
    [InlineData(true, 70)]
    [InlineData(false, 2752)]
    public async Task Part2(bool solveSample, int answer)
    {
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}