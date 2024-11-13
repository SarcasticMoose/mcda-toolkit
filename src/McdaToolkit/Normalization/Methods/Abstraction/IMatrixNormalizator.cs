using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Normalization.Methods.Abstraction;

/// <summary>
/// Matrix normalization generic abstraction
/// </summary>
internal interface IMatrixNormalizator<T> where T : struct, IEquatable<T>, IFormattable
{
    /// <summary>
    /// Normalize provided matrix
    /// </summary>
    /// <param name="matrix">One-dimensional vector of data to normalize</param>
    /// <param name="criteriaTypes">Describe type of vector, cost or profit</param>
    /// Return normalized matrix
    Matrix<T> NormalizeMatrix(Matrix<T> matrix, int[] criteriaTypes);
}