using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Abstraction;
using McdaToolkit.Normalization;
using McdaToolkit.Options;

namespace McdaToolkit.Mcda;

public class TopsisMethod : McdaMethod
{
    public TopsisMethod()
    {
        
    }
    
    public TopsisMethod(McdaMethodOptions options)
    { 
        DataNormalization = new DataNormalization(options.NormalizationMethod);
    }

    public override MathNet.Numerics.LinearAlgebra.Vector<double> Calculate(double[,] matrix, double[] weights,
        int[] criteriaDirections)
    {
        var convertedMatrix = Matrix<double>.Build.DenseOfArray(matrix);
        return Calculate(convertedMatrix,weights, criteriaDirections);
    }
    
    private MathNet.Numerics.LinearAlgebra.Vector<double> Calculate(Matrix<double> matrix, double[] weights, int[] criteriaDirections)
    {
        var normalizedMatrix = DataNormalization.NormalizeMatrix(matrix, criteriaDirections);
        var weightedMatrix = WeightedMatrix(normalizedMatrix, weights);

        var idealBest = IdealValues(weightedMatrix, true);
        var idealWorst = IdealValues(weightedMatrix, false);

        var distanceToBest = CalculateEuclideanDistance(weightedMatrix, idealBest);
        var distanceToWorst = CalculateEuclideanDistance(weightedMatrix, idealWorst);
        var topsisScores = CalculateTopsisScores(distanceToBest, distanceToWorst);

        return topsisScores;
    }
    private Matrix<double> WeightedMatrix(Matrix<double> matrix, double[] weights)
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
    private MathNet.Numerics.LinearAlgebra.Vector<double> IdealValues(Matrix<double> matrix, bool pis)
    {
        return MathNet.Numerics.LinearAlgebra.Vector<double>.Build
            .Dense(matrix.ColumnCount, i =>
            {
                var columnValues = matrix.Column(i).ToArray();
                return pis ? columnValues.Max() : columnValues.Min();
            });
    }
    private MathNet.Numerics.LinearAlgebra.Vector<double> CalculateEuclideanDistance(Matrix<double> matrix, MathNet.Numerics.LinearAlgebra.Vector<double> ideal)
    {
        return MathNet.Numerics.LinearAlgebra.Vector<double>.Build
            .DenseOfArray(matrix
                .EnumerateRows()
                .Select(row => row.Subtract(ideal)
                    .PointwisePower(2)
                    .PointwiseSqrt()
                    .Sum())
                .ToArray());
    }
    private MathNet.Numerics.LinearAlgebra.Vector<double> CalculateTopsisScores(MathNet.Numerics.LinearAlgebra.Vector<double> distanceToBest, MathNet.Numerics.LinearAlgebra.Vector<double> distanceToWorst)
    {
        return distanceToWorst
            .PointwiseDivide(distanceToBest.Add(distanceToWorst));
    }
}