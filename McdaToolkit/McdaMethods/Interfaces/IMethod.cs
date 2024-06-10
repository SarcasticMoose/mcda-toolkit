using LightResults;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.McdaMethods.Interfaces;

public interface IMethod
{
    Result<Vector<double>> Calculate(double[,] matrix, double[] weights, int[] criteriaDirections);
}