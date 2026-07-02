using System.Numerics;
using McdaToolkit.Core;
using McdaToolkit.Normalization.Transformers.Abstraction;

namespace McdaToolkit.Normalization.Transformers;

internal sealed class TransformerRegistry<T> : ITransformerRegistry<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly Dictionary<CriterionType, ICriterionTransformer<T>> _transformers = [];

    public TransformerRegistry()
    {
        _transformers[CriterionType.Benefit] = new ProfitTransformer<T>();
        _transformers[CriterionType.Cost] = new CostTransformer<T>();
    }

    public ICriterionTransformer<T> Get(CriterionType type)
    {
        if (!_transformers.TryGetValue(type, out var transformer))
        {
            throw new InvalidOperationException($"Unknown criterion type: {type}");
        }
        return transformer;
    }
}
