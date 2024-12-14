using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Extensions;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Normalization.Services.Abstraction;

namespace McdaToolkit.Mcda.Methods.Vikor;

public class Vikor : IVikorMethod
{
    private readonly IMatrixNormalizationService _matrixNormalizationService;
    private readonly VikorParameters _parameters;

    internal Vikor(
        IMatrixNormalizationService normalizationMatrixService,
        VikorParameters parameters)
    {
        _matrixNormalizationService = normalizationMatrixService;
        _parameters = parameters;
    }
    
    private IResult<VikorScore> ComputeScore(Matrix<double> matrix, Vector<double> weights, int[] types)
    {
        
        var normalizedMatrix = _matrixNormalizationService.NormalizeMatrix(matrix,types);

        var fStar = normalizedMatrix.GetColMax();
        var fMinus = normalizedMatrix.GetColMin();
        
        var weightedMatrix = matrix.MapIndexed((i, j, value) => weights[j] * (fStar[j] - value) / (fStar[j] - fMinus[j]));

        var s = weightedMatrix.RowSums();
        var r = weightedMatrix.Transpose().GetColMax();

        var sStar = s.Minimum();
        var sMinus = s.Maximum();
        var rStar = r.Minimum();
        var rMinus = r.Maximum();
        
        var sNormalized = (s - sStar) / (sMinus - sStar);
        var rNormalized = (r - rStar) / (rMinus - rStar);
        
        var q = _parameters.V * sNormalized + (1 - _parameters.V) * rNormalized;
        return Result.Ok(new VikorScore(s, r, q));
    }

    public IResult<VikorScore> Run(McdaInputData data)
    {
        return ComputeScore(data.Matrix,data.Weights,data.Types);
    }
    
    IResult IMcdaMethod.Run(McdaInputData data)
    {
        return Run(data);
    }
}