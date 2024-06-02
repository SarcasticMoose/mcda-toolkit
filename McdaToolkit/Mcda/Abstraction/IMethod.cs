namespace McdaToolkit.Mcda.Abstraction;

public interface IMethod
{
    MathNet.Numerics.LinearAlgebra.Vector<double> Calculate(double[,] matrix, double[] weights, int[] criteriaDirections);
}