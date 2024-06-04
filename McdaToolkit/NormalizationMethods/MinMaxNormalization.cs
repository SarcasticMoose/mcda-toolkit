using System.Numerics;
using McdaToolkit.NormalizationMethods.Interfaces;

namespace McdaToolkit.NormalizationMethods;

public class MinMaxNormalization : INormalizationMethod
{
    public MathNet.Numerics.LinearAlgebra.Vector<double> Normalize(MathNet.Numerics.LinearAlgebra.Vector<double> data, bool cost = false)
    {
        var max = data.Maximum();
        var min = data.Minimum();
        var difference = max - min;
        
        if (Math.Abs(difference) < 1.11e-16)
        {
            return MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(data.Count, (i) => 1);
        }
        
        if (cost)
        {
            return (max - data) / difference;
        }
        return (data - min) / difference;
    }
}