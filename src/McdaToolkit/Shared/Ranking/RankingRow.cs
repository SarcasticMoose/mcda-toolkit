namespace McdaToolkit.Shared.Ranking;

public record RankingRow<T> 
{
    public int Alternative { get; set; }
    public int Rank { get; set; }
    public T Score { get; set; }
}