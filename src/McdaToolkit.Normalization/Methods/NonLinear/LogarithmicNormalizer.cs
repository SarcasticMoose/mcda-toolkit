using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Types;

namespace McdaToolkit.Normalization.Methods.NonLinear;

internal class LogarithmicNormalizer<T> : IVectorNormalizer<T> where T : struct, IEquatable<T>, IFormattable
{
    private readonly IScalarMath<T> _scalarMath;

    public LogarithmicNormalizer(IScalarMath<T> scalarMath)
    {
        _scalarMath = scalarMath;
    }
    
    public Vector<T> Normalize(Vector<T> data)
    {
        var product = data.Aggregate(
            _scalarMath.One,
            (acc, x) => _scalarMath.Multiply(acc, x)
        );

        var denom = _scalarMath.Log(product);
        if (!_scalarMath.IsZero(denom))
        {
            var logValues = data.Map(x => _scalarMath.Log(x));
            return logValues.Map(x => _scalarMath.Divide(x, denom));
        }
        return data.Map(_ => _scalarMath.Zero);
    }
}