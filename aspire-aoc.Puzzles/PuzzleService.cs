namespace aspire_aoc.Puzzles;

public interface IPuzzleService
{
    public int Day { get; }
    
    public Task<int> SolvePart1();
    
    public Task<int> SolvePart2();
}