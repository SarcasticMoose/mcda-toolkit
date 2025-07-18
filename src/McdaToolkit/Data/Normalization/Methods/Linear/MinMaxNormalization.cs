using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Data.Normalization.Methods.Abstraction;

namespace McdaToolkit.Data.Normalization.Methods.Linear;

internal class MinMaxNormalization : IVectorNormalizator<double>
{
    private const double MaxTolerance = 1.11e-16;
    
    /// <inheritdoc cref="IVectorNormalizator{T}.Normalize"/>
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