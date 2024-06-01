using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaMethods.Normalization;

public interface IDataNormalization<TValue>
where TValue : struct, INumber<TValue>
{
    Matrix<TValue> NormalizeMatrix(Matrix<TValue> matrix, int[] criteriaTypes);
}