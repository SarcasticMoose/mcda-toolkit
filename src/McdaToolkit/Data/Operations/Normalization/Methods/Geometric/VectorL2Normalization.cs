using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Data.Operations.Normalization.Methods.Abstraction;

namespace McdaToolkit.Data.Operations.Normalization.Methods.Geometric;

internal class VectorL2Normalization : IVectorNormalizator<double>
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