using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Data.Normalization.Methods.Abstraction;

namespace McdaToolkit.Data.Normalization.Methods.Linear;

internal class MaxNormalization : IVectorNormalizator<double>
{
    /// <inheritdoc cref="IVectorNormalizator{T}.Normalize"/>
    public Vector<double> Normalize(Vector<double> data, bool cost)
    {
        if (cost)
        {
            return 1 - data / data.Maximum();
        }
        return data / data.Maximum();
    }
}