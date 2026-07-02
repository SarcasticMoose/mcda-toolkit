using System.Numerics;
using McdaToolkit.Normalization.Abstractions;
using McdaToolkit.Normalization.Normalizers;
using McdaToolkit.Normalization.Transformers;
using McdaToolkit.Normalization.Transformers.Abstraction;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Normalization.Steps;

/// <summary>Configures and builds a normalization step for an MCDA pipeline.</summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NormalizationStepBuilder{T}"/> class.
/// </remarks>
public sealed class NormalizationStepBuilder<T> where T : struct, IFloatingPointIeee754<T>
{
    private readonly INormalizerResolver<T> _resolver;
    private readonly ITransformerRegistry<T> _transformerRegistry;
    private IVectorNormalizer<T>? _vectorNormalizer;

    internal NormalizationStepBuilder(
        INormalizerResolver<T> resolver,
        ITransformerRegistry<T> transformerRegistry)
    {
        _resolver = resolver;
        _transformerRegistry = transformerRegistry;
    }


    /// <summary>Creates a new instance of <see cref="NormalizationStepBuilder{T}"/> with default normalizers and transformer registry.</summary>
    public static NormalizationStepBuilder<T> Create()
    {
        var resolver = new DefaultNormalizerResolver<T>([
            new MinMaxNormalizer<T>(),
            new MaxNormalizer<T>(),
            new SumNormalizer<T>(),
            new VectorL2Normalizer<T>(),
            new LogarithmicNormalizer<T>()
        ]);
        var transformerRegistry = new TransformerRegistry<T>();
        return new NormalizationStepBuilder<T>(resolver, transformerRegistry);
    }

    /// <summary>
    /// Specifies the normalization method to use for the step.
    /// </summary>
    public NormalizationStepBuilder<T> WithMethod(NormalizationMethod method)
    {
        var resolver = _resolver.Resolve(method);
        _vectorNormalizer = resolver;
        return this;
    }

    /// <summary>
    /// Builds the normalization step using the specified or default normalization method.
    /// </summary>
    internal IPreProcessingStep<T> Build()
    {
        _vectorNormalizer ??= _resolver.Resolve(NormalizationMethod.MinMax);
        return new NormalizationStep<T>(_vectorNormalizer, _transformerRegistry);
    }
}
