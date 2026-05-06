using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;

namespace McdaToolkit.Normalization.Methods.NonLinear;

internal class LogarithmicNormalizer<T> : IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        var product = data.Aggregate(T.One, (acc, x) => acc * x);
        var denom = T.Log(product);
        return T.IsZero(denom)
            ? data.Map(_ => T.Zero)
            : data.Map(x => T.Log(x) / denom);
    }
}
