using McdaToolkit.Normalization.Enums;

namespace McdaToolkit.Mcda.Methods.PrometheeII;

public record PrometheeIIOptions : IMcdaMethodOptions
{
    public NormalizationMethod NormalizationMethod { get; set; }
}