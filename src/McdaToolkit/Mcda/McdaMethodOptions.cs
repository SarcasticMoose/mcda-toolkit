using McdaToolkit.Normalization.Enums;

namespace McdaToolkit.Mcda;

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