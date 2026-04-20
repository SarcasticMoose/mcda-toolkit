using McdaToolkit.Normalization.Transformers;
using McdaToolkit.Normalization.Transformers.Abstraction;
using McdaToolkit.Types;

namespace McdaToolkit.Normalization;

public class TransformerRegistry<T> : ITransformerRegistry<T> where T : struct, IEquatable<T>, IFormattable
{
    private readonly IScalarMath<T> _scalarMath;
    private readonly IVectorAggregator<T> _vectorAggregator;

    public TransformerRegistry(
        IScalarMath<T> scalarMath,
        IVectorAggregator<T> vectorAggregator)
    {
        _scalarMath = scalarMath;
        _vectorAggregator = vectorAggregator;
    }
    
    public ICriterionTransformer<T> Get(CriterionType type)
        => type switch
        {
            CriterionType.Benefit => new ProfitTransformer<T>(),
            CriterionType.Cost => new CostTransformer<T>(_scalarMath, _vectorAggregator),
            _ => throw new ArgumentOutOfRangeException()
        };
}