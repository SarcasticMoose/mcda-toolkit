using System.Numerics;
using McdaToolkit.Normalization.Abstractions;
using McdaToolkit.Normalization.Transformers;
using McdaToolkit.Pipeline;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Normalization.Steps;

/// <summary>Configures and builds a normalization step for an MCDA pipeline.</summary>
public sealed class NormalizationStepBuilder<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly INormalizerResolver _resolver;
    private readonly ITransformerRegistry<T> _transformerRegistry;
    private IVectorNormalizer<T>? _vectorNormalizer;

    public NormalizationStepBuilder(
        INormalizerResolver resolver,
        ITransformerRegistry<T> transformerRegistry)
    {
        _resolver = resolver;
        _transformerRegistry = transformerRegistry;
    }
    
    public NormalizationStepBuilder<T> WithMethod(NormalizationMethod method)
    {
        var resolver = _resolver.Resolve<T>(method);
        _vectorNormalizer = resolver;   
        return this;
    }

    public IProcessingStep<T> Build()
    {
        _vectorNormalizer ??= _resolver.Resolve<T>(NormalizationMethod.MinMax);
        return new NormalizationStep<T>(_vectorNormalizer, _transformerRegistry);
    }
}