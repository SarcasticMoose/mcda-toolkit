using McdaToolkit.Data;
using McdaToolkit.Data.Normalization.Services.Abstraction;
using McdaToolkit.Extensions;
using McdaToolkit.Models.Abstraction;

namespace McdaToolkit.Models.School.European.Vikor;

public sealed class Vikor : McdaMethod<VikorScore>
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

    protected override IEnumerable<VikorScore> Execute(McdaInputData data)
    {
        var normalizedMatrix = _matrixNormalizationService.NormalizeMatrix(data.Matrix, data.Types);
        var fStar = normalizedMatrix.GetColMax();
        var fMinus = normalizedMatrix.GetColMin();

        var weightedMatrix = data.Matrix.MapIndexed(
            (i, j, value) => data.Weights[j] * (fStar[j] - value) / (fStar[j] - fMinus[j])
        );

        var s = weightedMatrix.RowSums();
        var r = weightedMatrix.Transpose().GetColMax();

        var sStar = s.Minimum();
        var sMinus = s.Maximum();
        var rStar = r.Minimum();
        var rMinus = r.Maximum();

        var sNormalized = (s - sStar) / (sMinus - sStar);
        var rNormalized = (r - rStar) / (rMinus - rStar);
        var q = _parameters.V * sNormalized + (1 - _parameters.V) * rNormalized;

        List<VikorScore> scores = new List<VikorScore>();

        for (int i = 0; i < q.Count; i++)
        {
            scores.Add(
                new VikorScore()
                {
                    Q = q[i],
                    R = r[i],
                    S = s[i],
                }
            );
        }
        return scores;
    }
}
