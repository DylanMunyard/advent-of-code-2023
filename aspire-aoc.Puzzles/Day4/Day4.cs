using aspire_aoc.Puzzles;

public class Day4 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    public Day4()
    {
        _puzzleService = new PuzzleService(this);
    }
    
    public int Score(string input)
    {
        var card = input.Split(": ")[0];
        var details = input.Split(": ")[1].Split(" | ");
        var winning = details[0].Trim().Split(new []{ "  ", " "}, StringSplitOptions.None).Select(x => int.Parse(x.Trim())).ToList();
        var numbers = details[1].Trim().Split(new []{ "  ", " "}, StringSplitOptions.None).Select(x => int.Parse(x.Trim())).ToList();

        return winning.Count(x => numbers.Contains(x));
    }

    public int WinningCards(string[] cards)
    {
        var weight = new Dictionary<int, int>();
        for (var card = 0; card < cards.Length; card++)
        {
            weight.Add(card, 1);
        }
        
        for (var card = 0; card < cards.Length; card++)
        {
            var score = Score(cards[card]);
            var multiplier = weight[card];
            for (var i = card + 1; i <= card + score ; i++)
            {
                if (i >= cards.Length) continue;
                weight[i] += multiplier;
            }
        }

        return weight.Values.Sum();
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return (await _puzzleService.InputAsLines(solveSample, 1)).Sum(x => (int)Math.Pow(2, Score(x) - 1)).ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        var cards = await _puzzleService.InputAsLines(solveSample, 2);
        return WinningCards(cards).ToString();
    }
}