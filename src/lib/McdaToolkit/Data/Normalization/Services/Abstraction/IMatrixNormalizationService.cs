using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Data.Normalization.Services.Abstraction;

internal interface IMatrixNormalizationService
{
    /// <summary>
    /// Normalize provided matrix
    /// </summary>
    /// <param name="matrix">One-dimensional vector of data to normalize</param>
    /// <param name="criteriaTypes">Describe type of vector, cost or profit</param>
    /// <returns>Normalized matrix</returns>
    Matrix<double> NormalizeMatrix(Matrix<double> matrix, int[] criteriaTypes);
}