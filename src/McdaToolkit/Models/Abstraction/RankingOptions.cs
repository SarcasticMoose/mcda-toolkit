namespace McdaToolkit.Models.Abstraction;

public record RankingOptions
{
    public bool OrderDescendingByScore { get; set; } = false;
}