using LightResults;
using McdaToolkit.Data.Normalization.Services.Abstraction;
using McdaToolkit.Extensions;
using McdaToolkit.Shared.Abstraction;
using McdaToolkit.Shared.Data;
using McdaToolkit.Shared.Ranking;

namespace McdaToolkit.Methods.Vikor;

public sealed class Vikor : IMcdaMethod<Ranking<VikorScore>>
{
    private readonly IMatrixNormalizationService _matrixNormalizationService;
    private readonly double _v;

    internal Vikor(
        IMatrixNormalizationService normalizationMatrixService,
        double v)
    {
        _matrixNormalizationService = normalizationMatrixService;
        _v = v;
    }
    
    public IResult<Ranking<VikorScore>> Run(McdaInputData data)
    {
        var normalizedMatrix = _matrixNormalizationService.NormalizeMatrix(data.Matrix,data.Types);
        var fStar = normalizedMatrix.GetColMax();
        var fMinus = normalizedMatrix.GetColMin();
        
        var weightedMatrix = data.Matrix.MapIndexed((i, j, value) => data.Weights[j] * (fStar[j] - value) / (fStar[j] - fMinus[j]));

        var s = weightedMatrix.RowSums();
        var r = weightedMatrix.Transpose().GetColMax();

        var sStar = s.Minimum();
        var sMinus = s.Maximum();
        var rStar = r.Minimum();
        var rMinus = r.Maximum();
        
        var sNormalized = (s - sStar) / (sMinus - sStar);
        var rNormalized = (r - rStar) / (rMinus - rStar);
        
        var q = _v * sNormalized + (1 - _v) * rNormalized;

        List<VikorScore> scores = new List<VikorScore>();
        
        for (int i = 0; i < q.Count; i++)
        {
            scores.Add(new VikorScore()
            {
                Q = q[i],
                R = r[i],   
                S = s[i]
            });
        }
        
        return Result.Success(scores.CreateRanking());
    }
}