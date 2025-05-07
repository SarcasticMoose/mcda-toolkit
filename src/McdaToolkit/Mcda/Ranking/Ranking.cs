using System.Collections;

namespace McdaToolkit.Mcda.Ranking;

public record Ranking<T> : IEnumerable<RankingRow<T>> where T : notnull, IComparable<T>
{
    public IEnumerable<RankingRow<T>> RankingItems { get; set; }
    public IEnumerator<RankingRow<T>> GetEnumerator()
    {
        return RankingItems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}