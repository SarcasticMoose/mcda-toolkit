using System.Numerics;

namespace McdaToolkit.Validation.Context;

internal readonly record struct RawMcdaInput<T>(
    T[,]? Matrix,
    List<CriterionDefinition<T>>? Criteria
) where T : struct, IFloatingPointIeee754<T>;
