namespace McdaToolkit.Fuzzy.Aggregator.Strategies.Abstract;

public interface IFuzzyMaxStrategy<T>
{
    T Max(IEnumerable<T> data);
}