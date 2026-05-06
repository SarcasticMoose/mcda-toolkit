namespace McdaToolkit.Fuzzy.Defuzzifiers;

public interface IDefuzzifier
{
    double Convert(TriangularFuzzyNumber x);
}