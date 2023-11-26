namespace aspire_aoc.Web;

public class PuzzleApiClient(HttpClient httpClient)
{
    public async Task<string?> GetSolution(int day, int part)
    {
        var solution = await httpClient.GetStringAsync($"/solve/{day}/part/{part}");
        return solution;
    }
}