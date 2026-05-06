using McdaToolkit.Types;

namespace McdaToolkit.Fuzzy.Defuzzifiers;

public class CentroidDefuzzifier : IDefuzzifier
{
    public double Convert(TriangularFuzzyNumber x)
        => (x.L + x.M + x.U) / 3.0;
}