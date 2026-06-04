using System.Numerics;

namespace McdaToolkit;

/// <summary>
/// Provides extension methods for <see cref="MathNet.Numerics.LinearAlgebra.Vector{T}"/> vectors.
/// </summary>
public static class VectorExtensions
{
    /// <summary>
    /// Computes the log product of the elements in the <see cref="MathNet.Numerics.LinearAlgebra.Vector{T}"/>.
    /// </summary>
    public static T LogProduct<T>(this MathNet.Numerics.LinearAlgebra.Vector<T> vector)
        where T : struct, IFloatingPointIeee754<T>
    {
        T result = T.Zero;
        for (int i = 0; i < vector.Count; i++)
        {
            result += T.Log(vector[i]);
        }
        return result;
    }
}
