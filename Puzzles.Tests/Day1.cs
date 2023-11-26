using aspire_aoc.Puzzles.Day1;
using FluentAssertions;

namespace Puzzles.Tests;

public class Day1Tests
{
    private readonly Day1 _puzzle;

    public Day1Tests()
    {
        _puzzle = new Day1();
    }
    
    [Fact]
    public async Task Part1()
    {
        // Act
        var solution = await _puzzle.SolvePart1();
        
        // Assert
        solution.Should().Be(69177);
    }
    
    [Fact]
    public async Task Part2()
    {
        // Act
        var solution = await _puzzle.SolvePart2();
        
        // Assert
        solution.Should().Be(207456);
    }
}