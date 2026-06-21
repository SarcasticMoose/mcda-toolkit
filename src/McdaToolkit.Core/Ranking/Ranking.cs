using System.Collections;
using System.Numerics;

namespace McdaToolkit.Core.Ranking;

/// <summary>
/// Represents a read-only ranking of alternatives, sorted in descending order.
/// </summary>
/// <typeparam name="T">
/// The type of the identifier or data associated with each alternative. Must implement <see cref="IComparable{T}"/>.
/// </typeparam>
public readonly record struct Ranking<T> : IEnumerable<RankingRow<T>>
    where T : struct, IFloatingPointIeee754<T>
{
    internal Ranking(IEnumerable<RankingRow<T>> rows) => RankingItems = [.. rows];

    /// <summary>
    /// Gets the collection of ranked alternatives as a read-only list.
    /// </summary>
    public IReadOnlyCollection<RankingRow<T>> RankingItems { get; }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public readonly IEnumerator<RankingRow<T>> GetEnumerator() => RankingItems.GetEnumerator();

    /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
    readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
