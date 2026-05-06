using System.Numerics;
using McdaToolkit.Models.Mcda;

namespace McdaToolkit.Validation.MatrixValidation.Context;

internal readonly record struct RawMcdaInput<T>(
    T[,]? Matrix,
    List<CriterionDefinition<T>>? Criteria
) where T : struct, IFloatingPointIeee754<T>;
