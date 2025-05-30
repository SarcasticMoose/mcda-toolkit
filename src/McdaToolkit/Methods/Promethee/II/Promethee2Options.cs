using McdaToolkit.Methods.Promethee.II.PreferenceFunctions.Factory;
using McdaToolkit.Normalization.Enums;
using McdaToolkit.Shared.Options;

namespace McdaToolkit.Methods.Promethee.II;

public record Promethee2Options : IMcdaMethodOptions
{
    public NormalizationMethod NormalizationMethod { get; set; } = NormalizationMethod.Vector;
    public PreferenceFunction PreferenceFunction { get; set; } = PreferenceFunction.Fshape;

    public bool UseGaia { get; set; } = false;
}