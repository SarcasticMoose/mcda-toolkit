using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Functions.FShape;

public class FShapePreferenceFunctionBuilder : 
    IPreferenceFunctionBuilder<IPreferenceFunction>
{
    private double _threshold;
    public FShapePreferenceFunctionBuilder WithPreferenceThreshold(double threshold)
    {
        if (threshold < 0.0 || threshold > 1.0)  throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold must be between 0 and 1.");
        _threshold = threshold;
        return this;
    }
    public IPreferenceFunction Build() => new FShapePreferenceFunction(_threshold);
}
