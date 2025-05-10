using System.Collections;

namespace McdaToolkit.Shared.Ranking;

public record Ranking<T> : IEnumerable<RankingRow<T>> 
    where T : IComparable<T>
{
    public IEnumerable<RankingRow<T>> RankingItems { get; set; }
    
    public IEnumerator<RankingRow<T>> GetEnumerator()
        => RankingItems.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
     => GetEnumerator();
}