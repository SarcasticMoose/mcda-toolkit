using System.Numerics;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization.Normalizers;

internal class SumNormalizer<T> : IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public NormalizationMethod Implements => NormalizationMethod.Sum;

    public MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        T sum = data.Sum();
        return T.IsZero(sum)
            ? data.Map(_ => T.Zero)
            : data.Map(x => x / sum);
    }
}
