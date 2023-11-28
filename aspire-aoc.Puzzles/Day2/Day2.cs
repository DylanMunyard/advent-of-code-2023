namespace aspire_aoc.Puzzles.Day2;

public class Day2 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    private Dictionary<string, int> _scores = new()
    {
        { "X", 1 },
        { "Y", 2 },
        { "Z", 3 }
    };

    // Fix in the "match fixing" sense"
    private Dictionary<string, Result> _fixedResults = new()
    {
        { "X", Result.Loss },
        { "Y", Result.Draw },
        { "Z", Result.Win }
    };

    private Dictionary<string, RockPaperScissors> _plays = new()
    {
        { "A", RockPaperScissors.Rock },
        { "X", RockPaperScissors.Rock },
        { "B", RockPaperScissors.Paper },
        { "Y", RockPaperScissors.Paper },
        { "C", RockPaperScissors.Scissors },
        { "Z", RockPaperScissors.Scissors }
    };

    private int Score(string play1, string play2)
    {
        if (LeftVsRight(play1, play2) == Result.Win) return _scores[play2]; // we lost

        if (LeftVsRight(play1, play2) == Result.Loss) return _scores[play2] + 6; // we won

        return _scores[play2] + 3; // draw
    }
    
    private Result LeftVsRight(string play1, string play2)
    {
        if (_plays[play1] == _plays[play2]) return Result.Draw;
        
        if ((_plays[play1] is RockPaperScissors.Rock && _plays[play2] is RockPaperScissors.Scissors) ||
            (_plays[play1] is RockPaperScissors.Paper && _plays[play2] is RockPaperScissors.Rock) ||
            (_plays[play1] is RockPaperScissors.Scissors && _plays[play2] is RockPaperScissors.Paper))
            return Result.Win;

        return Result.Loss;
    }

    private string FixResult(string play1, string fix)
    {
        return _fixedResults[fix] switch
        {
            Result.Draw => _plays[play1] switch
            {
                RockPaperScissors.Rock => "X",
                RockPaperScissors.Paper => "Y",
                RockPaperScissors.Scissors => "Z",
                _ => throw new ArgumentOutOfRangeException(nameof(play1), play1, null)
            },
            
            // fix a win
            Result.Win => _plays[play1] switch
            {
                RockPaperScissors.Rock => "Y", // paper beats rock
                RockPaperScissors.Paper => "Z", // scissors beats paper
                RockPaperScissors.Scissors => "X", // rock beats scissors
                _ => throw new ArgumentOutOfRangeException(nameof(play1), play1, null)
            },
            _ => _plays[play1] switch // fix a loss
            {
                RockPaperScissors.Rock => "Z", // rock beats scissors 
                RockPaperScissors.Paper => "X", // paper beats rock
                RockPaperScissors.Scissors => "Y", // scissors beats paper
                _ => throw new ArgumentOutOfRangeException(nameof(play1), play1, null)
            }
        };
    }

    private enum RockPaperScissors
    {
        Rock, 
        Paper,
        Scissors
    }

    private enum Result
    {
        Win, Draw, Loss
    }

    public Day2()
    {
        _puzzleService = new PuzzleService(this);
    }

    public async Task<int> SolvePart1(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample)).Sum(x => Score(x[..1], x[2..]));
    }

    public async Task<int> SolvePart2(bool solveSample)
    {
        return (await _puzzleService.PuzzleInput(solveSample)).Sum(x => Score(x[..1], FixResult(x[..1], x[2..])));
    }
}