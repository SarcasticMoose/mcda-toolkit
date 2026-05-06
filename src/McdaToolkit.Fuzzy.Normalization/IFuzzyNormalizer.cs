namespace McdaToolkit.Fuzzy.Normalization;

public interface IFuzzyNormalizer
{
    List<TriangularFuzzyNumber> Normalize(List<TriangularFuzzyNumber> data);
}