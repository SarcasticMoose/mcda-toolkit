using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.NormalizationMethods.Interfaces;

public interface INormalize<T> where T : struct, IEquatable<T>, IFormattable
{
    Vector<T> Normalize(Vector<T> data, bool cost);
}