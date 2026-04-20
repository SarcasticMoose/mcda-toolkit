using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Types;

namespace McdaToolkit.Normalization.Methods.Geometric;

internal class VectorL2Normalizer<T> : IVectorNormalizer<T> 
    where T : struct, IEquatable<T>, IFormattable
{
    private readonly IScalarMath<T> _scalarOperation;

    public VectorL2Normalizer(
        IScalarMath<T> scalarOperation)
    {
        _scalarOperation = scalarOperation;
    }

    public Vector<T> Normalize(Vector<T> data)
    {
        var sumOfSquares = data.Aggregate(
            _scalarOperation.Zero,
            (acc, x) => _scalarOperation.Add(acc, _scalarOperation.Multiply(x, x))
        );
        var norm = _scalarOperation.Sqrt(sumOfSquares);
        return !_scalarOperation.IsZero(norm) ? data.Map(x => _scalarOperation.Divide(x, norm)) : data.Map(_ => _scalarOperation.Zero);
    }
}