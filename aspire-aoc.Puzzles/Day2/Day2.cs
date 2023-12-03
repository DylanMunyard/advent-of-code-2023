using System.Text;
using System.Text.RegularExpressions;

namespace aspire_aoc.Puzzles.Day2;

public class Day2 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day2()
    {
        _puzzleService = new PuzzleService(this);
    }

    private record Play(int Blue, int Red, int Green);

    private readonly Regex _playregex = new Regex(@"(\d+) (\w+)");

    private (int game, IList<Play>) Game(string input)
    {
        var plays = new List<Play>();
        input = input.Replace("Game ", string.Empty);
        var game = int.Parse(input[..input.IndexOf(':')]); // get the game number
        input = input[input.IndexOf(':')..].Trim();
        var handfuls = input.Split(';'); // get each handful of cubes 
        foreach (var handful in handfuls)
        {
            var play = new Play(0, 0, 0);
            var cubes = handful.Split(",");
            foreach (var cube in cubes)
            {
                var match = _playregex.Matches(cube);
                var number = int.Parse(match[0].Groups[1].Value);
                var colour = match[0].Groups[2].Value;
                play = colour switch
                {
                    "blue" => play with { Blue = play.Blue + number },
                    "red" => play with { Red = play.Red + number },
                    "green" => play with { Green = play.Green + number },
                    _ => throw new Exception("Unknown colour")
                };
            }

            plays.Add(play);
        }

        return (game, plays);
    }

    private int MeetsCondition((int number, IList<Play> plays) game)
    {
        var (gameNumber, plays) = game;

        return plays.All(x => x.Red <= 12) &&
               plays.All(x => x.Green <= 13) &&
               plays.All(x => x.Blue <= 14)
            ? gameNumber
            : 0;
    }

    private int MinimumPlay((int number, IList<Play> plays) game)
    {
        var (_, plays) = game;
        var play = new Play(plays.Max(x => x.Blue), plays.Max(x => x.Red), plays.Max(x => x.Green));
        return play.Blue * play.Red * play.Green;
    }

    public async Task<int> SolvePart1(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample, 1)).Sum(x => MeetsCondition(Game(x)));
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample, 2)).Sum(x => MinimumPlay(Game(x)));
    }
}