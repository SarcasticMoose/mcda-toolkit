using System.Numerics;
using McdaToolkit.NormalizationMethods.Abstraction;

namespace McdaToolkit.NormalizationMethods;

public class MinMaxNormalization : INormalizationMethod
{
    public MathNet.Numerics.LinearAlgebra.Vector<double> Normalize(MathNet.Numerics.LinearAlgebra.Vector<double> data, bool cost = false)
    {
        double difference = data.Maximum() - data.Minimum();
        
        if (Math.Abs(difference) < double.Epsilon)
        {
            return MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(data.Count, (i) => 1);
        }
        
        if (cost)
        {
            return (data.Maximum() - data) / difference;
        }
        return (data - data.Minimum()) / difference;
    }
}