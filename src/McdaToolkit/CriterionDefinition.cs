namespace McdaToolkit;

/// <summary>Defines a single decision criterion with its name, type, and weight.</summary>
public record CriterionDefinition<T>
    where T : struct, IEquatable<T>, IFormattable
{
    /// <summary>Display name of the criterion.</summary>
    public string? Name { get; private set; }

    /// <summary>Whether the criterion is a benefit (maximize) or cost (minimize).</summary>
    public CriterionType Type { get; private set; }

    /// <summary>Relative importance weight of the criterion.</summary>
    public T Weight { get; private set; }

    private CriterionDefinition() { }

    internal static CriterionDefinition<T> Create(
        string? name,
        CriterionType type,
        T weight) => new()
    {
        Name = name,
        Type = type,
        Weight = weight
    };
}