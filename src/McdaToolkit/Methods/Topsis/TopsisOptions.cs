using McdaToolkit.Normalization.Enums;
using McdaToolkit.Shared.Options;

namespace McdaToolkit.Methods.Topsis;

public record TopsisOptions : IMcdaMethodOptions
{
    public NormalizationMethod NormalizationMethod { get; set; } = NormalizationMethod.MinMax;
}