using aspire_aoc.Puzzles.Day7;
using FluentAssertions;

namespace Puzzles.Tests;

public class Day7Tests
{
    private readonly Day7 _puzzle = new();

    [Theory]
    [InlineData(true, "6440")]
    [InlineData(false, "255048101")]
    public async Task Part1(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart1(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
    
    [Theory]
    [InlineData(true, "5905")]      // 254_567_844
    [InlineData(false, "253718286")] // 253_802_688 is too high
    public async Task Part2(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}