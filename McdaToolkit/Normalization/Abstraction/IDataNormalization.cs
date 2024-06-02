using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Normalization.Abstraction;

public interface IDataNormalization
{
    Matrix<double> NormalizeMatrix(Matrix<double> matrix, int[] criteriaTypes);
}