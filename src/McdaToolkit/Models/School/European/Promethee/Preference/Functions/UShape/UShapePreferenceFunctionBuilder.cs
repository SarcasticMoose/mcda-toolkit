using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;
using McdaToolkit.Models.School.European.Promethee.Preference.Threshold;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Functions.UShape;

public class UShapePreferenceFunctionBuilder : IPreferenceFunctionBuilder<IPreferenceFunction>
{
    private double? _q;

    public UShapePreferenceFunctionBuilder WithIndifferenceThreshold(double threshold)
    {
        if (threshold < 0.0 || threshold > 1.0)  throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold must be between 0 and 1.");
        _q = threshold;
        return this;
    }
    
    public IPreferenceFunction Build() => new UShapePreferenceFunction( _q ?? IndifferenceThreshold.Default);
}