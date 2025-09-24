using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Threshold;

/// <summary>
/// Represents the preference threshold used in preference functions.
/// Indicates the value above which one alternative is strictly preferred over another.
/// </summary>
internal record PreferenceThreshold : IPreferenceFunctionThreshold
{
    /// <summary>
    /// Gets the numeric value of the preference threshold.
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="PreferenceThreshold"/> with the specified value.
    /// </summary>
    /// <param name="value">The threshold value.</param>
    public PreferenceThreshold(double value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the default preference threshold value.
    /// </summary>
    internal static double Default => 1;
}