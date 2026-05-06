using McdaToolkit.Types;

namespace McdaToolkit.Fuzzy.Normalization.Normalizers;

public class FuzzyMaxNormalizer : IFuzzyNormalizer
{
    private readonly IFuzzyVectorAggregator<TriangularFuzzyNumber> _agg;

    public FuzzyMaxNormalizer(IFuzzyVectorAggregator<TriangularFuzzyNumber> agg)
    {
        _agg = agg;
    }

    public List<TriangularFuzzyNumber> Normalize(List<TriangularFuzzyNumber> data)
    {
        var max = _agg.Max(data);

        return data.Select(x =>
                new TriangularFuzzyNumber(
                    x.L / max.U,
                    x.M / max.M,
                    x.U / max.L))
            .ToList();
    }
}