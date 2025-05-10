using McdaToolkit.Methods.Promethee2.PreferenceFunctions.Factory;
using McdaToolkit.Normalization.Enums;
using McdaToolkit.Shared.Options;

namespace McdaToolkit.Methods.Promethee2;

public record Promethee2Options : IMcdaMethodOptions
{
    public NormalizationMethod NormalizationMethod { get; set; } = NormalizationMethod.Vector;
    public PreferenceFunction PreferenceFunction { get; set; } = PreferenceFunction.Fshape;
}