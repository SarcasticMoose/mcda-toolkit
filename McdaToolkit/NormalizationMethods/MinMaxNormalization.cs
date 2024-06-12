using McdaToolkit.NormalizationMethods.Interfaces;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.NormalizationMethods;

public class MinMaxNormalization : INormalizationMethod
{
    private const double MaxError = 1.11e-16;
    
    public Vector<double> Normalize(Vector<double> data, bool cost = false)
    {
        var max = data.Maximum();
        var min = data.Minimum();
        var difference = max - min;
        if (Math.Abs(difference) < MaxError)
        {
            return Vector<double>.Build.Dense(data.Count, (i) => 1);
        }
        
        if (cost)
        {
            return (max - data) / difference;
        }
        return (data - min) / difference;
    }
}