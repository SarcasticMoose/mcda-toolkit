namespace McdaToolkit.Shared.Ranking;

public record RankingRow<T> 
{
<<<<<<< HEAD
    internal RankingRow(
        int alternative, 
        int rank, 
        T score)
    {
        Alternative = alternative;
        Rank = rank;
        Score = score;
    }
    
    public int Alternative { get; }
    public int Rank { get; }
    public T Score { get; }
=======
    public int Alternative { get; set; }
    public int Rank { get; set; }
    public T Score { get; set; }
>>>>>>> cc9253a (feat: updated namespaces)
}