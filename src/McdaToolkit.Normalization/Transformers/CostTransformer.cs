using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Transformers.Abstraction;
using McdaToolkit.Types;

namespace McdaToolkit.Normalization.Transformers;

internal class CostTransformer<T> : ICriterionTransformer<T>
    where T : struct, IEquatable<T>, IFormattable
{
    private readonly IVectorAggregator<T> _vectorAggregator;
    private readonly IScalarMath<T> _scalarMath;

    public CostTransformer(
        IScalarMath<T> scalarMath,
        IVectorAggregator<T> vectorAggregator)
    {
        _vectorAggregator = vectorAggregator;
        _scalarMath = scalarMath;
    }

    public Vector<T> Transform(Vector<T> data)
    {
        var max = _vectorAggregator.Max(data);
        return data.Map(x => _scalarMath.Subtract(max, x));
    }
}