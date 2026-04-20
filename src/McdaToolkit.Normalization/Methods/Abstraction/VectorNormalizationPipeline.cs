using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Transformers.Abstraction;

namespace McdaToolkit.Normalization.Methods.Abstraction;

internal class VectorNormalizationPipeline<T>
    where T : struct, IEquatable<T>, IFormattable
{
    private readonly ICriterionTransformer<T> _transformer;
    private readonly IVectorNormalizer<T> _normalizer;

    public VectorNormalizationPipeline(
        ICriterionTransformer<T> transformer,
        IVectorNormalizer<T> normalizer)
    {
        _transformer = transformer;
        _normalizer = normalizer;
    }

    public Vector<T> Process(Vector<T> data)
    {
        var transformed = _transformer.Transform(data);
        return _normalizer.Normalize(transformed);
    }
}