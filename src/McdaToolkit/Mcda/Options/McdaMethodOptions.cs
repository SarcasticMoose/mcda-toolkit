using McdaToolkit.Enums;

namespace McdaToolkit.Mcda.Options;

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