namespace aspire_aoc.Puzzles.Day7;

public class Day7 : IPuzzleService
{
    private readonly PuzzleService _puzzleService;

    private IList<string> _p1Hands = new List<string>
        { "A", "K", "Q", "J", "T", "9", "8", "7", "6", "5", "4", "3", "2" };
    private IList<string> _p2Hands = new List<string>
        { "A", "K", "Q", "T", "9", "8", "7", "6", "5", "4", "3", "2", "J" };

    public Day7()
    {
        _puzzleService = new PuzzleService(this);
    }

    public (int P1, int P2) Solve(string input)
    {
        var hands = input
            .Split(Environment.NewLine) // each hand
            .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)) // hand and score
            .Select(x => (x[0], x[0], int.Parse(x[1]), x[0].ToCharArray().GroupBy(y => y).ToDictionary(g => g.Key, g => g.Count()))) // hand, score, count of cards
            .ToList();
        
        var i = hands.Count;
        hands.Sort((a, b) => CompareP1(a, b, _p1Hands));
        var p1 = hands.Sum(hand => hand.Item3 * i--);
        
        // Change the cards so they become strong
        var p2Hands = new List<(string, string, int, IDictionary<char, int>)>();
        foreach (var hand in hands)
        {
            p2Hands.Add(hand);
            if (!hand.Item4.ContainsKey('J')) continue;
            var jokers = hand.Item4['J'];
            
            // Apply the joker to the strongest other card, but keep the J itself
            var highestCards = hand.Item4.Where(x => x.Key != 'J').ToList();
            if (highestCards.Count == 0) continue;
            
            var highestCount = highestCards.Max(x => x.Value);
            var strongestCard = hand.Item4.Where(x => x.Key != 'J' && x.Value == highestCount).MinBy(x => _p2Hands.IndexOf(x.Key.ToString()));
            hand.Item4[strongestCard.Key] = strongestCard.Value + jokers;
            var newHand = (hand.Item1, hand.Item1.Replace('J', strongestCard.Key), hand.Item3, hand.Item4);
            p2Hands[hands.IndexOf(hand)] = newHand;
        }

        i = hands.Count;
        p2Hands.Sort((a, b) => CompareP1(a, b, _p2Hands));
        var p2 = p2Hands.Sum(hand => hand.Item3 * i--);
        
        return (p1, p2);
    }

    /// <summary>
    /// Every hand is exactly one type. From strongest to weakest, they are:
    /// Five of a kind, where all five cards have the same label: AAAAA
    /// Four of a kind, where four cards have the same label and one card has a different label: AA8AA
    /// Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
    /// Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
    /// Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
    /// One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
    /// High card, where all cards' labels are distinct: 23456
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="hands"></param>
    /// <returns></returns>
    private int CompareP1((string, string, int, IDictionary<char, int>) a,
        (string, string, int, IDictionary<char, int>) b, IList<string> hands)
    {
        var aKeys = a.Item2.ToCharArray().GroupBy(y => y).ToDictionary(g => g.Key, g => g.Count());
        var bKeys = b.Item2.ToCharArray().GroupBy(y => y).ToDictionary(g => g.Key, g => g.Count());
        if (aKeys.Count < bKeys.Count) return -1;
        if (bKeys.Count < aKeys.Count) return 1;
        if (aKeys.Count == bKeys.Count)  
        {
            var maxA = aKeys.Values.Max();
            var maxB = bKeys.Values.Max();

            if (maxA > maxB) return -1;
            if (maxB > maxA) return 1;
        }

        // They are the same type, compare positions
        for (var i = 0; i < a.Item1.Length; i++)
        {
            var aIndex = hands.IndexOf(a.Item1[i].ToString());
            var bIndex = hands.IndexOf(b.Item1[i].ToString());

            if (aIndex == bIndex) continue;
            if (aIndex > bIndex) return 1;
            return -1;
        }

        return 0;
    }

    public async Task<string> SolvePart1(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P1.ToString();
    }

    public async Task<string> SolvePart2(bool solveSample)
    {
        return Solve(await _puzzleService.InputAsString(solveSample, 1)).P2.ToString();
    }
}