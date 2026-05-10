using System.Numerics;

namespace McdaToolkit.Normalization.Transformers;

internal sealed class TransformerRegistry<T> : ITransformerRegistry<T>
    where T : struct, IFloatingPointIeee754<T>
{

    public ICriterionTransformer<T> Get(CriterionType type)
        => type switch
        {
            CriterionType.Benefit => new ProfitTransformer<T>(),
            CriterionType.Cost => new CostTransformer<T>(),
            _ => throw new ArgumentOutOfRangeException()
        };
}
