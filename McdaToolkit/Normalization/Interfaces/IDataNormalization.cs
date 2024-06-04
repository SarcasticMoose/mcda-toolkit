using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Normalization.Interfaces;

public interface IDataNormalization
{
    Matrix<double> NormalizeMatrix(Matrix<double>? matrix, int[] criteriaTypes);
}