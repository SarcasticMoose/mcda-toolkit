using McdaToolkit.Normalization.Transformers.Abstraction;

namespace McdaToolkit.Normalization;

public interface ITransformerRegistry<T> where T : struct, IEquatable<T>, IFormattable
{
    ICriterionTransformer<T> Get(CriterionType type);
}