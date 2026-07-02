using System.Numerics;
using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Core;
using McdaToolkit.Normalization.Abstractions;
using McdaToolkit.Normalization.Transformers.Abstraction;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Normalization.Steps;

internal sealed class NormalizationStep<T>(
    IVectorNormalizer<T> normalizer,
    ITransformerRegistry<T> transformerRegistry) : IPreProcessingStep<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly IVectorNormalizer<T> _normalizer = normalizer;
    private readonly ITransformerRegistry<T> _transformerRegistry = transformerRegistry;

    public Result<McdaProblem<T>> Execute(McdaProblem<T> mcdaProblem)
    {
        int rows = mcdaProblem.Data.RowCount;
        int cols = mcdaProblem.Data.ColumnCount;

        Matrix<T> result = Matrix<T>.Build.Dense(rows, cols);
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
