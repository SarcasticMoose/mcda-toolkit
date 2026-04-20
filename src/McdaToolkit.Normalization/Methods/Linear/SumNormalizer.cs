using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Types;

namespace McdaToolkit.Normalization.Methods.Linear;

internal class SumNormalizer<T> : IVectorNormalizer<T>
    where T : struct, IEquatable<T>, IFormattable
{
    private readonly IScalarMath<T> _scalarOperation;

    public SumNormalizer(    
        IScalarMath<T> scalarOperation)
    {
        _scalarOperation = scalarOperation;
    }

    public Vector<T> Normalize(Vector<T> data)
    {
        var sum = data.Aggregate(_scalarOperation.Zero, (acc, x) => _scalarOperation.Add(acc, x));
        return !_scalarOperation.IsZero(sum) ? data.Map(x => _scalarOperation.Divide(x, sum)) : data.Map(_ => _scalarOperation.Zero);
    }
}