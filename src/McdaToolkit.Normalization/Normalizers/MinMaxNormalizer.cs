using System.Numerics;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization.Normalizers;

internal class MinMaxNormalizer<T> : IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public NormalizationMethod Implements => NormalizationMethod.MinMax;

    public MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        T min = data.Minimum();
        return data.Map(x => (x - min) / (data.Maximum() - min));
    }
}
