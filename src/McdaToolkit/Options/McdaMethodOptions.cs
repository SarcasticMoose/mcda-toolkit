using McdaToolkit.Enums;

namespace McdaToolkit.Options;

/// <summary>
/// Configuration for Mcda methods
/// </summary>
public record McdaMethodOptions
{
    public NormalizationMethod NormalizationMethod { get; set; } = NormalizationMethod.MinMax;
}