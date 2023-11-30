namespace aspire_aoc.Puzzles.Day4;

public class Day4 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;
    
    public Day4()
    {
        _puzzleService = new PuzzleService(this);
    }

    private (Section, Section) Sections(string input)
    {
        var sections = input.Split(",");
        var section1 = sections[0].Split("-");
        var section2 = sections[1].Split("-");

        return (
            new Section(int.Parse(section1[0]), int.Parse(section1[1])),
            new Section(int.Parse(section2[0]), int.Parse(section2[1])));
    }

    private record Section(int Start, int End);

    private bool FullyOverlaps((Section, Section) sections)
    {
        var (section, otherSection) = sections;
        
        if (section.Start <= otherSection.Start && section.End >= otherSection.End) return true;
        if (otherSection.Start <= section.Start && otherSection.End >= section.End) return true;
        return false;
    }

    private bool AnyOverlap((Section, Section) sections)
    {
        var (section, otherSection) = sections;
        
        // Sort the coordinates
        var start1 = Math.Min(section.Start, section.End);
        var end1 = Math.Max(section.Start, section.End);
        var start2 = Math.Min(otherSection.Start, otherSection.End);
        var end2 = Math.Max(otherSection.Start, otherSection.End);

        // Check if the ranges overlap
        return Math.Max(start1, start2) <= Math.Min(end1, end2);
    }

    public async Task<int> SolvePart1(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample)).Count(x => FullyOverlaps(Sections(x)));
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample)).Count(x => AnyOverlap(Sections(x)));
    }
}