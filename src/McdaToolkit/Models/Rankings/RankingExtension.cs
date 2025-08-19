namespace McdaToolkit.Models.Rankings;

internal static class RankingExtension
{
    /// <summary>
    /// Creates a ranking of decision alternatives from a sequence of scored values,
    /// sorted in descending order by value (i.e., most preferred first),
    /// and assigns each item a rank and original alternative index.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the values to rank. Must implement <see cref="IComparable{T}"/>.
    /// </typeparam>
    /// <param name="scores">
    /// A sequence of values representing scores or preference measures for decision alternatives.
    /// </param>
    /// <returns>
    /// A <see cref="Ranking{T}"/> object representing the ranked alternatives,
    /// sorted by their original order (i.e., alternative index), with assigned ranks and scores.
    /// </returns>
    public static Ranking<TValue> CreateRanking<TValue>(this IEnumerable<TValue> scores) 
        where TValue : struct, IComparable<TValue>, IEquatable<TValue>
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