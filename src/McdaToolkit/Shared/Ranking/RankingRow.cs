namespace McdaToolkit.Shared.Ranking;

public record RankingRow<T> 
{
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
}