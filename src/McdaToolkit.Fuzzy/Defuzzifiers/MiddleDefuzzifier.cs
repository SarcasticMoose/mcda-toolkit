using McdaToolkit.Types;

namespace McdaToolkit.Fuzzy.Defuzzifiers;

public class MiddleDefuzzifier : IDefuzzifier
{
    public double Convert(TriangularFuzzyNumber x) => x.M;
}