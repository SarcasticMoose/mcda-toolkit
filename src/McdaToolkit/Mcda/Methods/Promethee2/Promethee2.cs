using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions.Abstraction;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Mcda.Methods.Promethee2;

public sealed class Promethee2 : McdaMethodBase<Promethee2Score>
{
    private readonly MatrixNormalizatorService _normalizationServiceServiceService;
    private readonly IPreferenceFunction _preferenceFunction;

    internal Promethee2(
        MatrixNormalizatorService matrixNormalizationServiceService,
        IPreferenceFunction preferenceFunction)
    {
        _normalizationServiceServiceService = matrixNormalizationServiceService;
        _preferenceFunction = preferenceFunction;
    }
    
    public override IResult<Promethee2Score> Run(McdaInputData data)
    {
        var normalizedMatrix = _normalizationServiceServiceService.NormalizeMatrix(data.Matrix, data.Types);
        var diffrentialMatrix = GetAlternativeDiffrence(normalizedMatrix);
        var afterPreferenceFunction = _preferenceFunction.Execute(diffrentialMatrix);
        var afterWeights = GetWeightedMatrix(afterPreferenceFunction, data.Weights);
        throw new NotImplementedException();
    }

    private Matrix<double> GetAlternativeDiffrence(Matrix<double> matrix)
    {
        var rows = matrix.RowCount;
        var cols = matrix.ColumnCount;  
        var alternativeMatrix = Matrix<double>.Build.Dense(rows * rows, cols);

        var iindex = 0;
        for (int i = 0; i < rows; i++)
        {
            var row = matrix.Row(i);
            for (int j = 1; j < rows; j++)
            {
                var nextRow = row - matrix.Row(j);
                alternativeMatrix.SetRow(iindex, nextRow);
                iindex++;
            }
        }
        return alternativeMatrix;
    }

    private Matrix<double> GetWeightedMatrix(Matrix<double> matrix, Vector<double> weights)
    {
        for (int i = 0; i < matrix.RowCount; i++)
        {
            matrix.SetRow(i, matrix.Row(i).PointwiseMultiply(weights));
        }
        return matrix;
    }
}