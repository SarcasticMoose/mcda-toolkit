
using System.Numerics;

namespace McdaMethods.Normalization.NormalizationMethods.Abstraction;

public interface INormalizationMethod<TValue>
where TValue : struct, INumber<TValue>
{
    MathNet.Numerics.LinearAlgebra.Vector<TValue> Normalize<T>(MathNet.Numerics.LinearAlgebra.Vector<TValue> data, bool cost);
}