using System.Numerics;
using McdaToolkit.Normalization.Transformers.Abstraction;

namespace McdaToolkit.Normalization.Abstractions;

internal class VectorNormalizationPipeline<T>(
    ICriterionTransformer<T> transformer,
    IVectorNormalizer<T> normalizer)
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly ICriterionTransformer<T> _transformer = transformer;
    private readonly IVectorNormalizer<T> _normalizer = normalizer;

    /// <summary>
    /// Processes the input vector by applying the normalization pipeline.
    /// </summary>
    /// <param name="data">The input vector to process.</param>
    /// <returns>The normalized vector.</returns>
    public MathNet.Numerics.LinearAlgebra.Vector<T> Process(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        var transformed = _transformer.Transform(data);
        return _normalizer.Normalize(transformed);
    }
}
