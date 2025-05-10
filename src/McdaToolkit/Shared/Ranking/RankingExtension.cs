namespace McdaToolkit.Shared.Ranking;

internal static class RankingExtension
{
    public static Ranking<TValue> CreateRanking<TValue>(this IEnumerable<TValue> scores) 
        where TValue : IComparable<TValue>
    {
<<<<<<< HEAD
        return new Ranking<TValue>(scores
            .Select((value, index) => new { index, value })
            .OrderByDescending(x => x.value)
            .Select((x, index) => new RankingRow<TValue>(
                x.index + 1,
                index + 1,
                x.value)
            )
            .OrderBy(x => x.Alternative)
            .ToList());
=======
        return new Ranking<TValue>
        {
            RankingItems = scores
                .Select((value, index) => new { index, value })
                .OrderByDescending(x => x.value)
                .Select((x, index) => new RankingRow<TValue>()
                {
                    Rank = index + 1,
                    Alternative = x.index + 1,
                    Score = x.value
                })
                .OrderBy(x => x.Alternative)
                .ToList()
        };
>>>>>>> cc9253a (feat: updated namespaces)
    }
}