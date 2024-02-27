
using MathNet.Numerics.LinearAlgebra;

namespace McdaMethods.NormalizationMethods;

public interface INormalizationMethod
{
    Vector<double> Normalize<T>(Vector<double> data, bool cost);
}