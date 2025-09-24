using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Functions.FShape;

public class FShapePreferenceFunctionBuilder : 
    IPreferenceFunctionBuilder<IPreferenceFunction>
{
    private double _threshold;
    
    /// <summary>
    /// Sets the indifference/preference threshold parameter for the PROMETHEE F-shape preference function.
    /// </summary>
    /// <param name="threshold">
    /// A value between 0 and 1 representing the preference threshold.
    /// 
    /// - Values close to 0 mean that even very small differences between alternatives
    ///   will generate a strong preference.
    /// - Values close to 1 mean that only large differences between alternatives
    ///   will be considered significant.
    /// </param>
    /// <returns>
    /// The current <see cref="FShapePreferenceFunctionBuilder"/> instance to allow fluent configuration.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="threshold"/> is outside the valid range [0, 1].
    /// </exception>
    /// <remarks>
    /// In the PROMETHEE method, the preference threshold controls how sensitive the
    /// decision-maker is to differences between alternatives. This parameter defines
    /// when the preference intensity starts to increase from indifference (0) towards
    /// strict preference (1).
    /// </remarks>
    public FShapePreferenceFunctionBuilder WithPreferenceThreshold(double threshold)
    {
        if (threshold is < 0.0 or > 1.0) 
            throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold must be between 0 and 1.");
        _threshold = threshold;
        return this;
    }
    
    /// <inheritdoc cref="IPreferenceFunctionBuilder{TPreferenceFunction}.Build"/>
    public IPreferenceFunction Build() => new FShapePreferenceFunction(_threshold);
}
