using McdaToolkit.NormalizationMethods.Interfaces;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.NormalizationMethods;

public class MinMaxNormalization : INormalizationMethod
{
    public Vector<double> Normalize(MathNet.Numerics.LinearAlgebra.Vector<double> data, bool cost = false)
    {
        var max = data.Maximum();
        var min = data.Minimum();
        var difference = max - min;
        if (Math.Abs(difference) < 1.11e-16)
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