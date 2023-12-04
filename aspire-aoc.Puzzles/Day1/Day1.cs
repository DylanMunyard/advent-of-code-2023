using System.Text.RegularExpressions;
using aspire_aoc.Puzzles;

public class Day1 : IPuzzleService
{
    private int WordToNum(string word)
    {
        return word switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => 0
        };
    }

    private readonly Regex _aNumber = new (@"\d");
    private readonly Regex _aNumberOrWord = new (@"(?=(one|two|three|four|five|six|seven|eight|nine|\d))");
    private readonly PuzzleService _puzzleService;

    private int Digits(string input)
    {
        var match = _aNumber.Matches(input);
        var number = 0;
        
        number += int.Parse(match[^1].Value) * (int)Math.Pow(10, 0);
        number += int.Parse(match[0].Value) * (int)Math.Pow(10, 1);

        return number;
    }

    private int DigitsOrWords(string input)
    {
        var match = _aNumberOrWord.Matches(input);
        var number = 0;
        
        if (int.TryParse(match[^1].Groups[1].Value, out var digit))
        {
            number += digit * (int)Math.Pow(10, 0);
        }
        else
        {
            number += WordToNum(match[^1].Groups[1].Value) * (int)Math.Pow(10, 0);
        }
        
        if (int.TryParse(match[0].Groups[1].Value, out digit))
        {
            number += digit * (int)Math.Pow(10, 1);
        }
        else
        {
            number += WordToNum(match[0].Groups[1].Value) * (int)Math.Pow(10, 1);
        }

        return number;
    }
    
    public Day1()
    {
        _puzzleService = new PuzzleService(this);
    }

    public async Task<int> SolvePart1(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample, 1)).Sum(Digits);
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample, 2)).Sum(DigitsOrWords);
    }
}