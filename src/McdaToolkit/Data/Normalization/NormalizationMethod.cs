namespace McdaToolkit.Data.Normalization;

/// <summary>
/// Specifies the method used to normalize criteria values in multi-criteria decision analysis (MCDA).
/// </summary>
public enum NormalizationMethod
{
    MinMax = 1,
    Vector = 2,
    Logarithmic = 3,
    Sum = 4,
    Max = 5
}