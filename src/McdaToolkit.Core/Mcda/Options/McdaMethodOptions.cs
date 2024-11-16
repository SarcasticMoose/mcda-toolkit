using McdaToolkit.Core.Enums;

namespace McdaToolkit.Core.Mcda.Options;

/// <summary>
/// Configuration for Mcda methods
/// </summary>
public record McdaMethodOptions
{
    /// <summary>
    /// Current normalization method
    /// </summary>
    public NormalizationMethod NormalizationMethod { get; set; } = NormalizationMethod.MinMax;
}