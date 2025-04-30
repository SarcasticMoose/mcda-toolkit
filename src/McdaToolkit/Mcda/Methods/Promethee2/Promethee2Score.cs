using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Methods.Promethee2;

public record Promethee2Score : IMcdaScore
{
    public IEnumerable<Promethee2ScoreRanking> Ranking { get; set; }
}

public record Promethee2ScoreRanking
{
    public int Alternative { get; set; }
    public int Rank { get; set; }
    public double Score { get; set; }
}