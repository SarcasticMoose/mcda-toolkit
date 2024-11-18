using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Extensions;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Providers;
using McdaToolkit.Normalization.Services.Abstraction;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Mcda.Methods.Vikor;

public class Vikor : IVikorMethod
{
    IMatrixNormalizationService _normalizationService;

    private Vikor(McdaMethodOptions options)
    {
        _normalizationService = new MatrixNormalizatorService(options.NormalizationMethod);
    }

    internal static Vikor Create(McdaMethodOptions options)
    {
        return new Vikor(options);
    }
    
    private Result<VikorScore> ComputeScore(Matrix<double> matrix, Vector<double> weights, int[] types, VikorParameters parameters)
    {
        var normalizedMatrix = _normalizationService.NormalizeMatrix(matrix,types);

        var fStar = normalizedMatrix.GetColMax();
        var fMinus = normalizedMatrix.GetColMin();
        
        var weightedF = matrix.MapIndexed((i, j, value) => weights[j] * (fStar[j] - value) / (fStar[j] - fMinus[j]));

        var s = weightedF.RowSums();
        var r = weightedF.Transpose().GetColMax();

        var sStar = s.Minimum();
        var sMinus = s.Maximum();
        var rStar = r.Minimum();
        var rMinus = r.Maximum();
        
        var sNormalized = (s - sStar) / (sMinus - sStar);
        var rNormalized = (r - rStar) / (rMinus - rStar);
        
        var q = parameters.V * sNormalized + (1 - parameters.V) * rNormalized;
        return Result.Ok(new VikorScore(s, r, q));
    }

    public IResult<VikorScore> Run(IDataProvider dataProvider)
    {
        var data = dataProvider.GetData();
        return ComputeScore(data.Matrix,data.Weights,data.Types,(VikorParameters)data.Parameters!);
    }
    
    IResult IMcdaMethod.Run(IDataProvider dataProvider)
    {
        return Run(dataProvider);
    }
}