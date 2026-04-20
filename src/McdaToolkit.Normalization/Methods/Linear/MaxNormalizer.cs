using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Types;

namespace McdaToolkit.Normalization.Methods.Linear;

internal class MaxNormalizer<T> : IVectorNormalizer<T> 
    where T : struct, IEquatable<T>, IFormattable
{
    private readonly IScalarMath<T> _scalarOperation;
    private readonly IVectorAggregator<T> _vectorAggregator;

    public MaxNormalizer(
        IScalarMath<T> scalarOperation,
        IVectorAggregator<T> vectorAggregator)
    {
        _scalarOperation = scalarOperation;
        _vectorAggregator = vectorAggregator;
    }

    public Vector<T> Normalize(Vector<T> data)
    {
        var max = _vectorAggregator.Max(data);
        return data.Map(x => _scalarOperation.Divide(x, max));
    }
}