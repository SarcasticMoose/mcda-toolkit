using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Data.Normalization.Methods.Abstraction;

namespace McdaToolkit.Data.Normalization.Methods.Linear;

internal class SumNormalization : IVectorNormalizator<double>
{
    /// <inheritdoc cref="IVectorNormalizator{T}.Normalize"/>
    public Vector<double> Normalize(Vector<double> data, bool cost)
    {
        if (cost)
        {
            return 1 / data / data.Sum(x => 1/x);
        }

        return data / data.Sum();
    }
}