using MathNet.Numerics.LinearAlgebra;

namespace McdaMethods.Helpers;

public interface IDataNormalization
{
    Matrix<double> NormalizeMatrix(Matrix<double> matrix, int[] criteriaTypes);
}