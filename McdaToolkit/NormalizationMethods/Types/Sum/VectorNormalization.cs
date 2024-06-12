using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.NormalizationMethods.Interfaces;

namespace McdaToolkit.NormalizationMethods.Types.Sum;

public class VectorNormalization : INormalize<double>
{
    /// <summary>
    /// Create normalized vector using vector normalization method 
    /// </summary>
    /// <param name="data">One-dimensional vector of data to normalize</param>
    /// <param name="cost">Describe type of vector, cost or profit</param>
    /// <returns>
    /// Return normalized vector
    /// </returns>
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