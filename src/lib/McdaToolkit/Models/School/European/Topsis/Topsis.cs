using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Data;
using McdaToolkit.Data.Normalization.Services.Abstraction;
using McdaToolkit.Data.Normalization.Services.MatrixNormalizator;
using McdaToolkit.Models.Abstraction;

namespace McdaToolkit.Models.School.European.Topsis;

public sealed class Topsis : McdaMethod<double>
{
    private readonly IMatrixNormalizationService _normalizationServiceServiceService;

    internal Topsis(MatrixNormalizatorService matrixNormalizationServiceService)
    {
        _normalizationServiceServiceService = matrixNormalizationServiceService;
    }

    private Vector<double> IdealValues(Matrix<double> matrix, bool pis)
    {
        return Vector<double>.Build.Dense(
            matrix.ColumnCount,
            i =>
            {
                var column = matrix.Column(i);
                return pis ? column.Max() : column.Min();
            }
        );
    }

    private Vector<double> CalculateEuclideanDistance(Matrix<double> matrix, Vector<double> point)
    {
        return Vector<double>.Build.DenseOfArray(
            matrix.EnumerateRows().Select(row => Distance.Euclidean(row, point)).ToArray()
        );
    }

    public override string Name => MethodStaticNames.Topsis;

    internal override IEnumerable<double> Execute(McdaInputData data)
    {
        var normalizedMatrix = _normalizationServiceServiceService.NormalizeMatrix(
            data.Matrix,
            data.Types
        );
        var weightedMatrix = normalizedMatrix.MapIndexed(
            (i, j, value) => data.Weights[j] * data.Matrix[i, j]
        );

        var idealBest = IdealValues(weightedMatrix, true);
        var idealWorst = IdealValues(weightedMatrix, false);

        var distanceToBest = CalculateEuclideanDistance(weightedMatrix, idealBest);
        var distanceToWorst = CalculateEuclideanDistance(weightedMatrix, idealWorst);
        var scores = distanceToWorst.PointwiseDivide(distanceToBest.Add(distanceToWorst));
        return scores;
    }
}
