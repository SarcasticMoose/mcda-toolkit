namespace McdaToolkit.Shared.Ranking;

internal static class RankingExtension
{
    public static Ranking<TValue> CreateRanking<TValue>(this IEnumerable<TValue> scores) 
        where TValue : IComparable<TValue>
    {
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
    }
}