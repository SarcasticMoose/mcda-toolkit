using McdaToolkit.Normalization.Enums;

namespace McdaToolkit.Mcda.Methods.Topsis;

public record TopsisOptions : IMcdaMethodOptions
{
    public NormalizationMethod NormalizationMethod { get; set; } = NormalizationMethod.MinMax;
}