using McdaToolkit.Normalization.Enums;
using McdaToolkit.Shared.Options;

namespace McdaToolkit.Methods.Vikor;

public record VikorOptions : IMcdaMethodOptions
{
    public VikorParameters VikorParameters { get; set; } = VikorParameters.CreateDefault();
    public NormalizationMethod NormalizationMethod { get; set; } = NormalizationMethod.Vector;
}