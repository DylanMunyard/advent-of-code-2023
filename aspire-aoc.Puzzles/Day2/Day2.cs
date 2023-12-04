using System.Text;
using System.Text.RegularExpressions;
using aspire_aoc.Puzzles;

public class Day2 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day2()
    {
        _puzzleService = new PuzzleService(this);
    }

    private record Play(int Blue, int Red, int Green);

    private (int Game, Play Play) Game(string input)
    {
        var details = input.Split(": ")[0];
        var game = int.Parse(details[details.IndexOf(' ')..]); // get the game number
        var handfuls = input.Split(": ")[1].Split(new []{"; ", ", ", " "}, StringSplitOptions.None);
        
        var play = new Play(0, 0, 0);
        for (var i = 0; i < handfuls.Length; i += 2)
        {
            var value = int.Parse(handfuls[i]);
            var colour = handfuls[i + 1];
            play = colour switch
            {
                "blue" => play with { Blue = value > play.Blue ? value : play.Blue },
                "red" => play with { Red = value > play.Red ? value : play.Red },
                "green" => play with { Green = value > play.Green ? value : play.Green },
                _ => throw new Exception("Unknown colour")
            };
        }

        return (game, play);
    }

    public async Task<int> SolvePart1(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample, 1))
            .Select(Game)
            .Where(game => game.Play is { Red: <= 12, Green: <= 13, Blue: <= 14 })
            .Sum(x => x.Game);
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample, 1))
            .Select(Game)
            .Sum(game => game.Play.Blue * game.Play.Red * game.Play.Green);
    }
}