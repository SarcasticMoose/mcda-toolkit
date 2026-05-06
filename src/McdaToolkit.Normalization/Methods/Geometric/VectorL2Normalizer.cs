using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;

namespace McdaToolkit.Normalization.Methods.Geometric;

internal class VectorL2Normalizer<T> : IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        var sumOfSquares = data.Aggregate(T.Zero, (acc, x) => acc + x * x);
        var norm = T.Sqrt(sumOfSquares);
        return T.IsZero(norm)
            ? data.Map(_ => T.Zero)
            : data.Map(x => x / norm);
    }
}
