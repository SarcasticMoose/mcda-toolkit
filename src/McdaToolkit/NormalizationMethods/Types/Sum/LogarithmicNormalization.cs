using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.NormalizationMethods.Interfaces;

namespace McdaToolkit.NormalizationMethods.Types.Sum;

public class LogarithmicNormalization : INormalize<double>
{
    public Vector<double> Normalize(Vector<double> data, bool cost)
    {
        var product  = data.Aggregate(1.0,(x,y) => x * y);
        var exp = data.PointwiseLog() / Math.Log(product);
        
        if (cost)
        {
            return 1 - exp;
        }

        return exp;
    }
}