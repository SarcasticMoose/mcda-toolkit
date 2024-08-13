using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.NormalizationMethods.Interfaces;

namespace McdaToolkit.NormalizationMethods.Types.Linear;

internal class MinMaxNormalization : INormalize<double>
{
    private const double MaxTolerance = 1.11e-16;
    
    /// <summary>
    /// Create normalized vector using vector normalization method 
    /// </summary>
    /// <param name="data">One-dimensional vector of data to normalize</param>
    /// <param name="cost">Describe type of vector, cost or profit</param>
    /// <returns>
    /// Return normalized vector
    /// </returns>
    public Vector<double> Normalize(Vector<double> data, bool cost = false)
    {
        var max = data.Maximum();
        var min = data.Minimum();
        var difference = max - min;
        
        if (Math.Abs(difference) < MaxTolerance)
        {
            return Vector<double>.Build.Dense(data.Count, _ => 1);
        }
        
        if (cost)
        {
            return (max - data) / difference;
        }
        return (data - min) / difference;
    }
}