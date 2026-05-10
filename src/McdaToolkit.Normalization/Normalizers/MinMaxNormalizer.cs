using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization.Normalizers;

internal class MinMaxNormalizer<T> : IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        var max = data.Max();
        var min = data.Min();
        return data.Map(x => (x - min) / (max - min));
    }
}
