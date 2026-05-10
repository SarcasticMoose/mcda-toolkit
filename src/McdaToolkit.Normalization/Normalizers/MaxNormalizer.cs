using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization.Normalizers;

internal class MaxNormalizer<T> : IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data) => data.Map(x => x / data.Max());
}
