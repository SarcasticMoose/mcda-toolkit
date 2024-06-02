using McdaToolkit.Enums;

namespace McdaToolkit.Options;

public class McdaMethodOptions
{
    public NormalizationMethod NormalizationMethod { get; set; } = NormalizationMethod.MinMax;
}