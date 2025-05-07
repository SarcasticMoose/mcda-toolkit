namespace McdaToolkit.Mcda.Ranking;

public record RankingRow<T> where T : notnull
{
    public int Alternative { get; set; }
    public int Rank { get; set; }
    public T Score { get; set; }
}