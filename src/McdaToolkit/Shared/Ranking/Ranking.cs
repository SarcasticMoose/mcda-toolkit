using System.Collections;

namespace McdaToolkit.Shared.Ranking;

public record Ranking<T> : IEnumerable<RankingRow<T>> 
    where T : IComparable<T>
{
<<<<<<< HEAD
    internal Ranking(List<RankingRow<T>> rows)
    {
        RankingItems = rows.ToArray();
    }
    
    public IReadOnlyCollection<RankingRow<T>> RankingItems { get; }

=======
    public IEnumerable<RankingRow<T>> RankingItems { get; set; }
    
>>>>>>> cc9253a (feat: updated namespaces)
    public IEnumerator<RankingRow<T>> GetEnumerator()
        => RankingItems.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
     => GetEnumerator();
}