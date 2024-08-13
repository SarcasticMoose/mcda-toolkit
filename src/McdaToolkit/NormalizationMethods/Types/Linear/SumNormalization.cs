using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.NormalizationMethods.Interfaces;

namespace McdaToolkit.NormalizationMethods.Types.Linear;

internal class SumNormalization : INormalize<double>
{
    public Vector<double> Normalize(Vector<double> data, bool cost)
    {
        if (cost)
        {
            return 1 / data / data.Sum(x => 1/x);
        }

        return data / data.Sum();
    }
}