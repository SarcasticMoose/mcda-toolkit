using System.Collections;

namespace McdaToolkit.Models.Rankings;

/// <summary>
/// Represents a read-only ranking of alternatives, sorted in descending order.
/// </summary>
/// <typeparam name="T">
/// The type of the identifier or data associated with each alternative. Must implement <see cref="IComparable{T}"/>.
/// </typeparam>
public readonly record struct Ranking<T> : IEnumerable<RankingRow<T>> 
    where T : struct, IEquatable<T>, IComparable<T>
{
    internal Ranking(List<RankingRow<T>> rows)
    {
        RankingItems = rows.ToArray();
    }
    
    /// <summary>
    /// Gets the collection of ranked alternatives as a read-only list.
    /// </summary>
    public IReadOnlyCollection<RankingRow<T>> RankingItems { get; }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public IEnumerator<RankingRow<T>> GetEnumerator()
        => RankingItems.GetEnumerator();

    /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
    IEnumerator IEnumerable.GetEnumerator()
     => GetEnumerator();
}