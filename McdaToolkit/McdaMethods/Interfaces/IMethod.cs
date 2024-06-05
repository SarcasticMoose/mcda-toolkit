using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.McdaMethods.Interfaces;

public interface IMethod
{
    Vector<double> Calculate(double[,] matrix, double[] weights, int[] criteriaDirections);
    Vector<double> Calculate(IEnumerable<IEnumerable<double>> matrix, double[] weights, int[] criteriaDirections);
}