using McdaToolkit.Fuzzy.Aggregator.Strategies.Abstract;
using McdaToolkit.Types;

namespace McdaToolkit.Fuzzy.Aggregator.Strategies;

public class FuzzyMinByMiddleStrategy : IFuzzyMinStrategy<TriangularFuzzyNumber>
{
    public TriangularFuzzyNumber Min(IEnumerable<TriangularFuzzyNumber> data)
        => data.Aggregate((a, b) => a.M <= b.M ? a : b);
}