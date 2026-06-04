using System.Numerics;
using McdaToolkit.Normalization.Abstractions;
using McdaToolkit.Normalization.Transformers.Abstraction;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Normalization.Steps;

/// <summary>Configures and builds a normalization step for an MCDA pipeline.</summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NormalizationStepBuilder{T}"/> class.
/// </remarks>
public sealed class NormalizationStepBuilder<T>(
    INormalizerResolver<T> resolver,
    ITransformerRegistry<T> transformerRegistry)
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly INormalizerResolver<T> _resolver = resolver;
    private readonly ITransformerRegistry<T> _transformerRegistry = transformerRegistry;
    private IVectorNormalizer<T>? _vectorNormalizer;

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
    public IProcessingStep<T> Build()
    {
        _vectorNormalizer ??= _resolver.Resolve(NormalizationMethod.MinMax);
        return new NormalizationStep<T>(_vectorNormalizer, _transformerRegistry);
    }
}
