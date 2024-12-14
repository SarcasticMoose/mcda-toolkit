using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;

namespace McdaToolkit.Normalization.Methods;

internal class LogarithmicNormalization : IVectorNormalizator<double>
{
    /// <inheritdoc cref="IVectorNormalizator{T}.Normalize"/>
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