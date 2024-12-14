using McdaToolkit.Normalization.Enums;

namespace McdaToolkit.Mcda.Methods.Vikor;

public record VikorOptions : IMcdaMethodOptions
{
    public VikorParameters VikorParameters { get; set; } = VikorParameters.CreateDefault();
    public NormalizationMethod NormalizationMethod { get; set; } = NormalizationMethod.Vector;
}