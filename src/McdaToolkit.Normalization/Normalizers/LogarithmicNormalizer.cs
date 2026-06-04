using System.Numerics;
using McdaToolkit.Core;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization.Normalizers;

internal class LogarithmicNormalizer<T> : IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public NormalizationMethod Implements => NormalizationMethod.Logarithmic;

    public MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        var denom = data.LogProduct();
        return T.IsZero(denom)
            ? data.Map(_ => T.Zero)
            : data.Map(x => T.Log(x) / denom);
    }
}
