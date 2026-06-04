using System.Numerics;

namespace McdaToolkit.Validation.Context;

internal readonly record struct ValidMcdaInput<T>(
    T[,] Matrix,
    CriterionDefinition<T>[] Criteria
) where T : struct, IFloatingPointIeee754<T>;
