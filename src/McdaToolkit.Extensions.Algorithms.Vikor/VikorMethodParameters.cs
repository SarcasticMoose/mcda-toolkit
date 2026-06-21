using System.Numerics;

namespace McdaToolkit.Extensions.Algorithms.Vikor;

/// <summary>
/// Represents the parameters used in the VIKOR method.
/// </summary>
public record VikorMethodParameters<T>
where T : struct, IFloatingPointIeee754<T>
{
    private VikorMethodParameters(T v)
    {
        V = v;
    }
    
    public static VikorMethodParameters<T> Default => Create(T.One / (T.One + T.One));

    /// <summary>
    /// Gets the weight of the strategy of the majority of criteria (v), in the range [0.0, 1.0].
    /// </summary>
    public T V { get; }

    /// <summary>
    /// Creates a new instance of <see cref="VikorMethodParameters{T}"/> with the specified V parameter.
    /// </summary>
    /// <param name="v">The weight of the strategy of the majority of criteria. Must be between 0.0 and 1.0 inclusive.</param>
    /// <returns>A new <see cref="VikorMethodParameters{T}"/> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="v"/> is not in the range [0.0, 1.0].</exception>
    internal static VikorMethodParameters<T> Create(T v)
    {
        if (v < T.Zero || v > T.One)
        {
            throw new ArgumentOutOfRangeException(
                nameof(v),
                v,
                "Value must be between 0.0 and 1.0."
            );
        }
        return new VikorMethodParameters<T>(v);
    }
}
