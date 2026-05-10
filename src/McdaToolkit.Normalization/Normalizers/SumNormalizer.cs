using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization.Normalizers;

internal class SumNormalizer<T> : IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        var sum = data.Aggregate(T.Zero, (acc, x) => acc + x);
        return T.IsZero(sum)
            ? data.Map(_ => T.Zero)
            : data.Map(x => x / sum);
    }
}
