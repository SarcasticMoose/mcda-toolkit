using MathNet.Numerics.LinearAlgebra;

namespace McdaMethods.MCDA;

public interface IMethod
{
    Vector<double> Calculate(Matrix<double> matrix, double[] weights, int[] criteriaDirections);
}