using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Data.Operations.Normalization.Service.MatrixNormalizator;

internal interface IMatrixNormalizator
{
    /// <summary>
    /// Normalize provided matrix
    /// </summary>
    /// <param name="matrix">One-dimensional vector of data to normalize</param>
    /// <param name="criteriaTypes">Describe type of vector, cost or profit</param>
    /// <returns>Normalized matrix</returns>
    Matrix<double> Normalize(Matrix<double> matrix, int[] criteriaTypes);
}