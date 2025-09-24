namespace McdaToolkit.Models.School.European.Vikor;

/// <summary>
/// Represents the parameters used in the VIKOR method.
/// </summary>
public record VikorParameters
{
    /// <summary>
    /// Gets the weight of the strategy of the majority of criteria (v), in the range [0.0, 1.0].
    /// </summary>
    public double V { get; }

    /// <summary>
    /// Creates a new instance of <see cref="VikorParameters"/> with the specified V parameter.
    /// </summary>
    /// <param name="v">The weight of the strategy of the majority of criteria. Must be between 0.0 and 1.0 inclusive.</param>
    /// <returns>A new <see cref="VikorParameters"/> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="v"/> is not in the range [0.0, 1.0].</exception>
    public static VikorParameters Create(double v)
    {
        if (v is < 0.0 or > 1.0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(v),
                v,
                "Value must be between 0.0 and 1.0."
            );
        }
        return new VikorParameters(v);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VikorParameters"/> class.
    /// </summary>
    /// <param name="v">The value of the V parameter.</param>
    private VikorParameters(double v)
    {
        V = v;
    }
}
