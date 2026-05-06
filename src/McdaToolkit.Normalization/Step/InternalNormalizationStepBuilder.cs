using System.Numerics;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Normalization.Transformers;
using McdaToolkit.Pipeline;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Normalization.Step;

internal sealed class InternalNormalizationStepBuilder<T> : INormalizationStepBuilder<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly INormalizerResolver _resolver;
    private readonly ITransformerRegistry<T> _transformerRegistry;
    private IVectorNormalizer<T>? _vectorNormalizer;

    public InternalNormalizationStepBuilder(
        INormalizerResolver resolver,
        ITransformerRegistry<T> transformerRegistry)
    {
        _resolver = resolver;
        _transformerRegistry = transformerRegistry;
    }
    
    public INormalizationStepBuilder<T> WithMethod(NormalizationMethod method)
    {
        var resolver = _resolver.Resolve<T>(method);
        _vectorNormalizer = resolver;   
        return this;
    }

    public IPreProcessingStep<T> Build()
    {
        _vectorNormalizer ??= _resolver.Resolve<T>(NormalizationMethod.MinMax);
        return new NormalizationStep<T>(_vectorNormalizer, _transformerRegistry);
    }
}