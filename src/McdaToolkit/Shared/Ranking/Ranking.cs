using System.Collections;

namespace McdaToolkit.Shared.Ranking;

public record Ranking<T> : IEnumerable<RankingRow<T>> 
    where T : IComparable<T>
{
    internal Ranking(List<RankingRow<T>> rows)
    {
        RankingItems = rows.ToArray();
    }
    
    public IReadOnlyCollection<RankingRow<T>> RankingItems { get; }

    public IEnumerator<RankingRow<T>> GetEnumerator()
        => RankingItems.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
     => GetEnumerator();
}