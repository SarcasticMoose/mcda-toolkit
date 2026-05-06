using McdaToolkit.Fuzzy.Aggregator.Strategies.Abstract;
using McdaToolkit.Types;

namespace McdaToolkit.Fuzzy.Aggregator;

public class FuzzyVectorAggregator : IFuzzyVectorAggregator<TriangularFuzzyNumber>
{
    private readonly IFuzzyMinStrategy<TriangularFuzzyNumber> _fuzzyMinStrategy;
    private readonly IFuzzyMaxStrategy<TriangularFuzzyNumber> _fuzzyMaxStrategy;

    public FuzzyVectorAggregator(
        IFuzzyMinStrategy<TriangularFuzzyNumber> fuzzyMinStrategy,
        IFuzzyMaxStrategy<TriangularFuzzyNumber> fuzzyMaxStrategy)
    {
        _fuzzyMinStrategy = fuzzyMinStrategy;
        _fuzzyMaxStrategy = fuzzyMaxStrategy;
    }
    
    public TriangularFuzzyNumber Max(IEnumerable<TriangularFuzzyNumber> data)
        => data.Aggregate((a, b) => a.M >= b.M ? a : b);

    public TriangularFuzzyNumber Min(IEnumerable<TriangularFuzzyNumber> data)
        => data.Aggregate((a, b) => a.M <= b.M ? a : b);
}