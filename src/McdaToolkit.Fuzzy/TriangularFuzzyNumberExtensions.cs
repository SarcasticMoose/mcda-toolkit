using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Fuzzy.Defuzzifiers;

namespace McdaToolkit.Fuzzy;

public static class TriangularFuzzyNumberExtensions
{
    public static Vector<double> ToVector(
        IEnumerable<TriangularFuzzyNumber> data,
        IDefuzzifier defuzzifier)
    {
        return Vector<double>.Build.Dense(data.Select(defuzzifier.Convert).ToArray());
    }
}