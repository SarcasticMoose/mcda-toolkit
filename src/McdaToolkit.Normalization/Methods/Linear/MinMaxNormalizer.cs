using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Types;

namespace McdaToolkit.Normalization.Methods.Linear;

internal class MinMaxNormalizer<T> : IVectorNormalizer<T> 
    where T : struct, IEquatable<T>, IFormattable
{
    private readonly IScalarMath<T> _scalarOperation;
    private readonly IVectorAggregator<T> _vectorAggregator;
    
    public MinMaxNormalizer(    
        IScalarMath<T> scalarOperation,
        IVectorAggregator<T> vectorAggregator)
    {
        _scalarOperation = scalarOperation;
        _vectorAggregator = vectorAggregator;
    }

    public Vector<T> Normalize(Vector<T> data)
    {
        var max = _vectorAggregator.Max(data);
        var min = _vectorAggregator.Min(data);
        var diff = _scalarOperation.Subtract(max, min);
        return data.Map(x =>
            _scalarOperation.Divide(
                _scalarOperation.Subtract(x, min),
                diff
            )
        );
    }
}