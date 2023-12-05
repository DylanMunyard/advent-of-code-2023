using FluentAssertions;

namespace Puzzles.Tests;

public class Day4Tests
{
    private readonly Day4 _puzzle = new();

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
    [InlineData(false, "14427616")]
    public async Task Part2(bool solveSample, string answer)
    {
        // Act
        var solution = await _puzzle.SolvePart2(solveSample);
        
        // Assert
        solution.Should().Be(answer);
    }
}