using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Threshold;

/// <summary>
/// Represents the indifference threshold used in preference functions.
/// Indicates the value below which two alternatives are considered equivalent.
/// </summary>
internal record IndifferenceThreshold : IPreferenceFunctionThreshold
{
    /// <summary>
    /// Gets the numeric value of the indifference threshold.
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="IndifferenceThreshold"/> with the specified value.
    /// </summary>
    /// <param name="value">The threshold value.</param>
    public IndifferenceThreshold(double value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the default indifference threshold value.
    /// </summary>
    internal static double Default => 0;
}