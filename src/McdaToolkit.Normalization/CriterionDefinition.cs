namespace McdaToolkit.Normalization;

public record CriterionDefinition<T> where T : struct, IEquatable<T>, IFormattable
{
    public string Name { get; init; }
    public CriterionType Type { get; init; }
}