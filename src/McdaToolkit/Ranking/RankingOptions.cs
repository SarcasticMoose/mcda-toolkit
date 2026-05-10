namespace McdaToolkit.Ranking;

/// <summary>Controls how the final ranking is ordered.</summary>
public record RankingOptions
{
    /// <summary>When <see langword="true"/>, alternatives are sorted by score descending instead of by index.</summary>
    public bool OrderDescendingByScore { get; set; } = false;

    /// <summary>Number of decimal places to round scores to before ranking. <see langword="null"/> disables rounding.</summary>
    public int? Precision { get; set; } = null;
}