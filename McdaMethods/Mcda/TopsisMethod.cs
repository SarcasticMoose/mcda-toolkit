using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaMethods.Normalization;
using McdaMethods.Normalization.NormalizationMethods.Abstraction;

namespace McdaMethods.Mcda;

public class TopsisMethod<TValue>(INormalizationMethod<TValue> normalizationMethod) : IMethod<TValue>
where TValue : struct, INumber<TValue>
{
    private readonly IDataNormalization<TValue> _dataNormalization = new DataNormalization<TValue>(normalizationMethod);
    public MathNet.Numerics.LinearAlgebra.Vector<TValue> Calculate(Matrix<TValue> matrix, TValue[] weights, int[] criteriaDirections)
    {
        var normalizedMatrix = _dataNormalization.NormalizeMatrix(matrix, criteriaDirections);
        var weightedMatrix = WeightedMatrix(normalizedMatrix, weights);

        var idealBest = IdealValues(weightedMatrix, true);
        var idealWorst = IdealValues(weightedMatrix, false);

        var distanceToBest = CalculateEuclideanDistance(weightedMatrix, idealBest);
        var distanceToWorst = CalculateEuclideanDistance(weightedMatrix, idealWorst);
        var topsisScores = CalculateTopsisScores(distanceToBest, distanceToWorst);

        return topsisScores;
    }
    private Matrix<TValue> WeightedMatrix(Matrix<TValue> matrix, TValue[] weights)
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
    private MathNet.Numerics.LinearAlgebra.Vector<TValue> IdealValues(Matrix<TValue> matrix, bool pis)
    {
        return MathNet.Numerics.LinearAlgebra.Vector<TValue>.Build
            .Dense(matrix.ColumnCount, i =>
            {
                var columnValues = matrix.Column(i).ToArray();
                return pis ? columnValues.Max() : columnValues.Min();
            });
    }
    private MathNet.Numerics.LinearAlgebra.Vector<TValue> CalculateEuclideanDistance(Matrix<TValue> matrix, MathNet.Numerics.LinearAlgebra.Vector<TValue> ideal)
    {
        return MathNet.Numerics.LinearAlgebra.Vector<TValue>.Build
            .DenseOfArray(matrix
                .EnumerateRows()
                .Select(row => row.Subtract(ideal)
                    .PointwisePower((TValue)Convert.ChangeType(2,typeof(TValue)))
                    .PointwiseSqrt()
                    .Sum())
                .ToArray());
    }
    private MathNet.Numerics.LinearAlgebra.Vector<TValue> CalculateTopsisScores(MathNet.Numerics.LinearAlgebra.Vector<TValue> distanceToBest, MathNet.Numerics.LinearAlgebra.Vector<TValue> distanceToWorst)
    {
        return distanceToWorst
            .PointwiseDivide(distanceToBest.Add(distanceToWorst));
    }
}