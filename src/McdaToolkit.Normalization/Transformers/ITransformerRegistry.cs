using System.Numerics;

namespace McdaToolkit.Normalization.Transformers;

/// <summary>Provides the appropriate criterion transformer for a given <see cref="CriterionType"/>.</summary>
public interface ITransformerRegistry<T> where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>Returns the transformer for the specified criterion type.</summary>
    ICriterionTransformer<T> Get(CriterionType type);
}