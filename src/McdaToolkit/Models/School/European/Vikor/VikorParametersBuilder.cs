namespace McdaToolkit.Models.School.European.Vikor;

public sealed class VikorParametersBuilder
{
    private double _v;

    private VikorParametersBuilder(){}
    
    /// <summary>
    /// Creates a new instance of the <see cref="VikorParametersBuilder"/>.
    /// </summary>
    /// <returns>A new <see cref="VikorParametersBuilder"/> instance.</returns>
    public static VikorParametersBuilder Create() => new VikorParametersBuilder();

    /// <summary>
    /// Sets the value of the V parameter used in VIKOR calculation.
    /// </summary>
    /// <param name="v">The weight of the strategy of the majority of criteria (range [0, 1]).</param>
    /// <returns>The updated <see cref="VikorParametersBuilder"/> instance.</returns>
    public VikorParametersBuilder WithV(double v)
    {
        _v = v;
        return this;    
    }

    /// <summary>
    /// Builds and returns a <see cref="VikorParameters"/> instance with the configured parameters.
    /// </summary>
    /// <returns>A new <see cref="VikorParameters"/> instance.</returns>
    public VikorParameters Build()
    {
        return VikorParameters.Create(_v);
    }
}