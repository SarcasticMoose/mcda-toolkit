namespace McdaToolkit.Fuzzy.Aggregator.Strategies.Abstract;

public interface IFuzzyMinStrategy<T>
{
    T Min(IEnumerable<T> data);
}