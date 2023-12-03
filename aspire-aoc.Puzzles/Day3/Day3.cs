using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileProviders.Physical;

namespace aspire_aoc.Puzzles.Day3;

public class Day3 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day3()
    {
        _puzzleService = new PuzzleService(this);
        _input = Array.Empty<string>();
        _parts = new List<Part>();
    }

    private string[] _input;
    private List<Part> _parts;
    
    private record Part(int Line, int Start, int End);

    private List<Part> Parts()
    {
        var parts = new List<Part>();
        
        for (var line = 0; line < _input.Length; line++)
        {
            var start = -1;
            for (var pos = 0; pos < _input[line].Length; pos++)
            {
                if (!int.TryParse(_input[line][pos].ToString(), out var number))
                {
                    if (start > -1)
                    {
                        parts.Add(new Part(line, start, pos-1));
                    }
                    start = -1;
                    continue;
                }

                if (start > -1)
                {
                    continue;
                }
                
                start = pos;
            }
            
            if (start > -1)
            {
                parts.Add(new Part(line, start, _input[line].Length - 1));
            }
        }

        return parts;
    }
    
    private List<Part> Asterisks()
    {
        var symbols = new List<Part>();
        
        for (var line = 0; line < _input.Length; line++)
        {
            for (var pos = 0; pos < _input[line].Length; pos++)
            {
                if (_input[line][pos].ToString() == "*")
                {
                    symbols.Add(new Part(line, pos, pos));
                }
            }
        }

        return symbols;
    }

    private HashSet<Part> AdjacentParts(Part symbol)
    {
        var parts = new List<Part>();
        
        parts.AddRange(GetAdjacentParts(symbol.Line, symbol.Start - 1));
        parts.AddRange(GetAdjacentParts(symbol.Line, symbol.Start + 1));
        parts.AddRange(GetAdjacentParts(symbol.Line - 1, symbol.Start));
        parts.AddRange(GetAdjacentParts(symbol.Line - 1, symbol.Start - 1));
        parts.AddRange(GetAdjacentParts(symbol.Line - 1, symbol.Start + 1));
        parts.AddRange(GetAdjacentParts(symbol.Line + 1, symbol.Start));
        parts.AddRange(GetAdjacentParts(symbol.Line + 1, symbol.Start - 1));
        parts.AddRange(GetAdjacentParts(symbol.Line + 1, symbol.Start + 1));

        return parts.ToHashSet();

        IEnumerable<Part> GetAdjacentParts(int line, int pos)
        {
            return _parts.Where(x => x.Line == line && x.Start <= pos && x.End >= pos).ToList();
        }
    }

    private int GearRatio(HashSet<Part> parts)
    {
        return parts.Select(PartToNum).Aggregate((x, y) => x * y);
    }

    private bool SymbolAdjacent(Part part)
    {
        for (var i = part.Start; i <= part.End; i++)
        {
            if (Check(part.Line, i - 1, IsSymbol) ||
                Check(part.Line, i + 1, IsSymbol) ||
                Check(part.Line - 1, i, IsSymbol) ||
                Check(part.Line - 1, i - 1, IsSymbol) ||
                Check(part.Line - 1, i + 1, IsSymbol) ||
                Check(part.Line + 1, i, IsSymbol) ||
                Check(part.Line + 1, i - 1, IsSymbol) ||
                Check(part.Line + 1, i + 1, IsSymbol)) return true;
        }

        return false;
    }

    private int PartToNum(Part part)
    {
        var result = 0;
        var pow = 0;
        for (var i = part.End; i >= part.Start; i--)
        {
            var number = int.Parse(_input[part.Line][i].ToString());
            result += number * (int)Math.Pow(10, pow++);
        }

        return result;
    }

    private bool Check(int line, int pos, Func<char, bool> func)
    {
        try
        {
            return func(_input[line][pos]);
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }

    private readonly Regex _symbolRegex = new Regex(@"[^\d|^\.]");
    private bool IsSymbol(char input)
    {
        return _symbolRegex.IsMatch(input.ToString());
    }

    public async Task<int> SolvePart1(bool solveSample)
    {
        _input = await _puzzleService.PuzzleInput(solveSample, 1);
        return Parts().Where(SymbolAdjacent).Sum(PartToNum);
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        _input = await _puzzleService.PuzzleInput(solveSample, 2);
        _parts = Parts();
        var symbols = 
            Asterisks().Select(AdjacentParts).Where(x => x.Count == 2).Sum(GearRatio);

        return symbols;
    }
}