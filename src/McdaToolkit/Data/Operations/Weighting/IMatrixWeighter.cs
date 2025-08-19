using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Data.Operations.Weighting;

internal interface IMatrixWeighter
{
    /// <summary>
    /// Weight provided matrix
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="weights"></param>
    /// <returns></returns>
    Matrix<double> Weight(Matrix<double> matrix, Vector<double> weights);
}