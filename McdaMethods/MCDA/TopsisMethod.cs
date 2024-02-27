using MathNet.Numerics.LinearAlgebra;
using McdaMethods.Helpers;

namespace McdaMethods.MCDA;

public class TopsisMethod : IMethod
{
    private readonly IDataNormalization _dataNormalization;

    public TopsisMethod(IDataNormalization dataNormalization)
    {
        _dataNormalization = dataNormalization;
    }

    public Vector<double> Calculate(Matrix<double> matrix, double[] weights, int[] criteriaDirections)
    {
        Matrix<double>? normalizedMatrix = _dataNormalization.NormalizeMatrix(matrix, criteriaDirections);
        Matrix<double>? weightedMatrix = WeightedMatrix(normalizedMatrix, weights);

        Vector<double> idealBest = IdealValues(weightedMatrix, true);
        Vector<double> idealWorst = IdealValues(weightedMatrix, false);

        Vector<double> distanceToBest = CalculateEuclideanDistance(weightedMatrix, idealBest);
        Vector<double> distanceToWorst = CalculateEuclideanDistance(weightedMatrix, idealWorst);
        Vector<double> topsisScores = CalculateTopsisScores(distanceToBest, distanceToWorst);

        return topsisScores;
    }
    static Matrix<double> WeightedMatrix(Matrix<double> matrix, double[] weights)
    {
        for (int i = 0; i < matrix.RowCount; i++)
        {
            for (int j = 0; j < matrix.ColumnCount; j++)
            {
                matrix[i, j] *= weights[j];
            }
        }

        return matrix;
    }

    static Vector<double> IdealValues(Matrix<double> matrix, bool pis)
    {
        return Vector<double>.Build.Dense(matrix.ColumnCount, i =>
        {
            var columnValues = matrix.Column(i).ToArray();
            return pis ? columnValues.Max() : columnValues.Min();
        });
    }

    static Vector<double> CalculateEuclideanDistance(Matrix<double> matrix, Vector<double> ideal)
    {
        return Vector<double>.Build.DenseOfArray(
            matrix.EnumerateRows().Select(row =>
                Math.Sqrt(row.Subtract(ideal).PointwisePower(2).Sum()))
                .ToArray());
    }

    static Vector<double> CalculateTopsisScores(Vector<double> distanceToBest, Vector<double> distanceToWorst)
    {
        return distanceToWorst.PointwiseDivide(distanceToBest.Add(distanceToWorst));
    }
}