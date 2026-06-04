using McdaToolkit.Core.Ranking;

namespace McdaToolkit.Core;

/// <summary>Options that control MCDA method execution behaviour.</summary>
public record McdaExecutionOptions
{
    /// <summary>Ranking output configuration.</summary>
    public RankingOptions RankingOptions { get; set; } = new();
}