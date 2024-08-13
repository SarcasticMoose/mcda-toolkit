using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;

namespace McdaToolkit.Normalization.Methods.Sum;

internal class VectorNormalization : INormalizationMethod
{
    /// <inheritdoc cref="IVectorNormalizator{T}.Normalize"/>
    public Vector<double> Normalize(Vector<double> data, bool cost)
    {
        var squaresOfSum = data / Math.Sqrt(data.PointwisePower(2).Sum());
        if (cost)
        {
            return 1 - squaresOfSum;
        }
        return squaresOfSum;
    }
}