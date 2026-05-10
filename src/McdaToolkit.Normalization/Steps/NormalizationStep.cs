using System.Numerics;
using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Abstractions;
using McdaToolkit.Normalization.Transformers;
using McdaToolkit.Pipeline;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Normalization.Steps;

internal sealed class NormalizationStep<T> : IProcessingStep<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly IVectorNormalizer<T> _normalizer;
    private readonly ITransformerRegistry<T> _transformerRegistry;

    public NormalizationStep(
        IVectorNormalizer<T> normalizer,
        ITransformerRegistry<T> transformerRegistry)
    {
        _normalizer = normalizer;
        _transformerRegistry = transformerRegistry;
    }

    public Result<McdaProblem<T>> Process(McdaProblem<T> mcdaProblem)
    {
        var rows = mcdaProblem.Data.RowCount;
        var cols = mcdaProblem.Data.ColumnCount;

        var result = Matrix<T>.Build.Dense(rows, cols);
        for (int j = 0; j < cols; j++)
        {
            var column = mcdaProblem.Data.Column(j);
            var transformer = _transformerRegistry.Get(mcdaProblem.Criteria[j].Type);
            var normalized = new VectorNormalizationPipeline<T>(transformer, _normalizer).Process(column);
            for (int i = 0; i < rows; i++)
            {
                result[i, j] = normalized[i];
            }
        }
        return mcdaProblem with { Data = result };
    }
}