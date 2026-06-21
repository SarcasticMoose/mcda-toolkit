namespace McdaToolkit.Extensions.Algorithms.Vikor;

/// <summary>
/// Builder for creating <see cref="VikorMethodParameters{T}"/> instances.
/// </summary>
/// <typeparam name="T">The type of the result values.</typeparam>
public sealed class VikorMethodParametersBuilder<T>
    where T : struct, System.Numerics.IFloatingPointIeee754<T>
{
    private T _v;

    internal VikorMethodParametersBuilder() { }

    /// <summary>
    /// Sets the value of the V parameter used in VIKOR calculation.
    /// </summary>
    /// <param name="v">The weight of the strategy of the majority of criteria (range [0, 1]).</param>
    /// <returns>The updated <see cref="VikorMethodParametersBuilder{T}"/> instance.</returns>
    public VikorMethodParametersBuilder<T> WithV(T v)
    {
        _v = v;
        return this;
    }

    /// <summary>
    /// Builds and returns a <see cref="VikorMethodParameters{T}"/> instance with the configured parameters.
    /// </summary>
    /// <returns>A new <see cref="VikorMethodParameters{T}"/> instance.</returns>
    internal VikorMethodParameters<T> Build()
    {
        return VikorMethodParameters<T>.Create(_v);
    }
}
