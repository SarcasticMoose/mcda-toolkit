using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.NormalizationMethods.Interfaces;


public interface INormalize<T> where T : struct, IEquatable<T>, IFormattable
{
    /// <summary>
    /// Normalize vector of data
    /// </summary>
    /// <param name="data"></param>
    /// <param name="cost"></param>
    /// <returns>One-dimensional vector contains normalized values</returns>
    Vector<T> Normalize(Vector<T> data, bool cost);
}