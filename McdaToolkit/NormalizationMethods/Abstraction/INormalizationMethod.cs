
using System.Numerics;

namespace McdaToolkit.NormalizationMethods.Abstraction;

public interface INormalizationMethod
{
    MathNet.Numerics.LinearAlgebra.Vector<double> Normalize(MathNet.Numerics.LinearAlgebra.Vector<double> data, bool cost);
}