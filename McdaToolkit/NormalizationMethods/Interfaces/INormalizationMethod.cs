
namespace McdaToolkit.NormalizationMethods.Interfaces;

public interface INormalizationMethod
{
    MathNet.Numerics.LinearAlgebra.Vector<double> Normalize(MathNet.Numerics.LinearAlgebra.Vector<double> data, bool cost);
}