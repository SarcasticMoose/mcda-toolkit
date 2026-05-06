namespace McdaToolkit.Normalization;

/// <summary>
/// Specifies the method used to normalize criteria values in multi-criteria decision analysis (MCDA).
/// </summary>
public enum NormalizationMethod
{
    /// <summary>Scales values to [0, 1] using column min and max.</summary>
    MinMax = 1,

    /// <summary>Divides each value by the L2 norm of its column.</summary>
    Vector = 2,

    /// <summary>Normalizes using the logarithm of each value relative to the column product.</summary>
    Logarithmic = 3,

    /// <summary>Divides each value by the column sum.</summary>
    Sum = 4,

    /// <summary>Divides each value by the column maximum.</summary>
    Max = 5,
}