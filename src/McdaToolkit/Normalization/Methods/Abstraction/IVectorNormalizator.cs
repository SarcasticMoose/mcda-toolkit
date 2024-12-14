using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Normalization.Methods.Abstraction;

/// <summary>
/// Column normalization generic abstraction
/// </summary>
internal interface IVectorNormalizator<T> where T : struct, IEquatable<T>, IFormattable
{
    /// <summary>
    /// Normalize provided vector
    /// </summary>
    /// <param name="data">One-dimensional vector of data to normalize</param>
    /// <param name="cost">Describe type of vector, cost or profit</param>
    /// <returns>
    /// Return normalized vector
    /// </returns>
    Vector<T> Normalize(Vector<T> data, bool cost);
}