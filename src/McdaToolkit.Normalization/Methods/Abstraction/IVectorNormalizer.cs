using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Normalization.Methods.Abstraction;

public interface IVectorNormalizer<T>
    where T : struct, IEquatable<T>, IFormattable
{
    Vector<T> Normalize(Vector<T> data);
}