using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.NormalizationMethods.Interfaces;

namespace McdaToolkit.NormalizationMethods.Types.Linear;

public class MinMaxNormalization : INormalize<double>
{
    private const double MaxError = 1.11e-16;
    
    public Vector<double> Normalize(Vector<double> data, bool cost = false)
    {
        var max = data.Maximum();
        var min = data.Minimum();
        var difference = max - min;
        
        if (Math.Abs(difference) < MaxError)
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