using LightResults;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Data;
using McdaToolkit.Data.Operations;
using McdaToolkit.Data.Operations.Normalization.Service.MatrixNormalizator;
using McdaToolkit.Models.Abstraction;
using McdaToolkit.Models.Rankings;

namespace McdaToolkit.Models.School.European.Topsis;

public sealed class Topsis : IMcdaMethod<Ranking<double>>
{
    private readonly IMatrixNormalizator _matrixOperations;
    
    internal Topsis(MatrixOperations matrixMatrixOperations)
    {
        _matrixOperations = matrixMatrixOperations;
    }
    
    private Vector<double> IdealValues(
        Matrix<double> matrix,
        bool pis)
    {
        return Vector<double>
            .Build
            .Dense(matrix.ColumnCount, i =>
            {
                var column = matrix.Column(i);
                return pis ? column.Max() : column.Min();
            });
    }

    private Vector<double> CalculateEuclideanDistance(
        Matrix<double> matrix,
        Vector<double> point)
    {
        return Vector<double>
            .Build
            .DenseOfArray(matrix
                .EnumerateRows()
                .Select(row => Distance.Euclidean(row, point))
                .ToArray());
    }

    public IResult<Ranking<double>> Run(McdaInputData data)
    {
        var normalizedMatrix = _matrixOperations.Normalize(data.Matrix, data.Types);
        var weightedMatrix = normalizedMatrix.MapIndexed((i, j, value) => data.Weights[j] * data.Matrix[i, j]);

        var idealBest = IdealValues(weightedMatrix, true);
        var idealWorst = IdealValues(weightedMatrix, false);

        var distanceToBest = CalculateEuclideanDistance(weightedMatrix, idealBest);
        var distanceToWorst = CalculateEuclideanDistance(weightedMatrix, idealWorst);
        var scores = distanceToWorst.PointwiseDivide(distanceToBest.Add(distanceToWorst));

        return Result.Success(scores.CreateRanking());
    }
}