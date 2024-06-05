using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.McdaMethods.Interfaces;

namespace McdaToolkit.McdaMethods;

public abstract class McdaMethod : IMethod
{
    public Vector<double> Calculate(double[,] matrix, double[] weights, int[] criteriaDirections)
    {
        var matrixTypeOfMatrix = Matrix<double>.Build.DenseOfArray(matrix);
        return Calculate(matrixTypeOfMatrix, weights, criteriaDirections);
    }

    public Vector<double> Calculate(IEnumerable<IEnumerable<double>> matrix, double[] weights, int[] criteriaDirections)
    {
        var matrixTypeOfMatrix = Matrix<double>.Build.DenseOfRows(matrix);
        return Calculate(matrixTypeOfMatrix, weights, criteriaDirections);
    }

    protected abstract Vector<double> Calculate(Matrix<double> matrix, double[] weights, int[] criteriaDirections);
}