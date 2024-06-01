using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaMethods.Mcda;

public interface IMethod<TValue>
    where TValue : struct, INumber<TValue>
{
    MathNet.Numerics.LinearAlgebra.Vector<TValue> Calculate(Matrix<TValue> matrix, TValue[] weights, int[] criteriaDirections);
}