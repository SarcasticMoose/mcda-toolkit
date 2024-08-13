using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.NormalizationMethods.Interfaces;

namespace McdaToolkit.NormalizationMethods.Types.Linear;

internal class MaxNormalization : INormalize<double>
{
    /// <summary>
    /// Create normalized vector using max normalization method 
    /// </summary>
    /// <param name="data">One-dimensional vector of data to normalize</param>
    /// <param name="cost">Describe type of vector, cost or profit</param>
    /// <returns>
    /// Return normalized vector
    /// </returns>
    public Vector<double> Normalize(Vector<double> data, bool cost)
    {
        if (cost)
        {
            return 1 - data / data.Maximum();
        }

        return data / data.Maximum();
    }
}