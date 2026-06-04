using System.Numerics;

namespace McdaToolkit;

public static class VectorExtensions
{
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
