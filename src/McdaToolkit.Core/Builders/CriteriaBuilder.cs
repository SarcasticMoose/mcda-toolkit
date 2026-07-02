using System.Numerics;

namespace McdaToolkit.Core.Builders;

/// <summary>Fluent builder for creating a <see cref="CriterionDefinition{T}"/>.</summary>
public class CriteriaBuilder<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private T _weight;
    private CriterionType _type;
    private string? _name;

    /// <summary>Sets the criterion weight.</summary>
    public CriteriaBuilder<T> WithWeight(T weight)
    {
        _weight = weight;
        return this;
    }

    /// <summary>Sets the criterion type (benefit or cost).</summary>
    public CriteriaBuilder<T> WithType(CriterionType type)
    {
        _type = type;
        return this;
    }

    /// <summary>Sets the criterion name. Name is optional</summary>
    public CriteriaBuilder<T> WithName(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>Creates the <see cref="CriterionDefinition{T}"/> from the configured values.</summary>
    internal CriterionDefinition<T> Build()
    {
        return CriterionDefinition<T>.Create(_name, _type, _weight);
    }
}