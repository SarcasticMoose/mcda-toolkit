using System.Numerics;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization.Normalizers;

internal class VectorL2Normalizer<T> : IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public NormalizationMethod Implements => NormalizationMethod.Vector;

    public MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        var norm = T.Zero;
        for (int i = 0; i < data.Count; i++)
        {
            norm += data[i] * data[i];
        }
        norm = T.Sqrt(norm);
        return T.IsZero(norm)
            ? data.Map(_ => T.Zero)
            : data.Map(x => x / norm);
    }
}
