using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Ranking;

public class RankingFactory
{
    public Ranking<T> CreateRanking<T>(IEnumerable<T> scores) where T : IComparable<T>
    {
        return new Ranking<T>
        {
            RankingItems = scores
                .Select((value, index) => new { index, value })
                .OrderByDescending(x => x.value)
                .Select((x, index) => new RankingRow<T>()
                {
                    Rank = index + 1,
                    Alternative = x.index + 1,
                    Score = x.value
                })
                .OrderBy(x => x.Alternative)
                .ToList()
        };
    }
}