using System.Numerics;

namespace McdaToolkit.Core.Ranking;

/// <summary>
/// Provides extension methods for ranking decision alternatives.
/// </summary>
public static class RankingExtension
{
    /// <summary>
    /// Creates a ranking of decision alternatives from a sequence of scored values,
    /// sorted in descending order by value (i.e., most preferred first),
    /// and assigns each item a rank and original alternative index.
    /// </summary>
    public static Ranking<TValue> CreateRanking<TValue>(
        this TValue[] scores,
        RankingOptions options
    )
        where TValue : struct, IFloatingPointIeee754<TValue>
    {
        var enumerableRanking = scores
            .Select((value, index) => new
            {
                index,
                value = options.Precision.HasValue
                    ? TValue.Round(value, options.Precision.Value, MidpointRounding.AwayFromZero)
                    : value
            })
            .OrderByDescending(x => x.value)
            .Select((x, index) => new RankingRow<TValue>(x.index + 1, index + 1, x.value));

        enumerableRanking = options.OrderDescendingByScore
            ? enumerableRanking.OrderByDescending(x => x.Score)
            : enumerableRanking.OrderBy(x => x.Alternative);

        return new Ranking<TValue>([.. enumerableRanking]);
    }
}
