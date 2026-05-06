using System.Numerics;
using McdaToolkit.Models.Mcda;

namespace McdaToolkit.Validation.MatrixValidation.Context;

internal readonly record struct ValidMcdaInput<T>(
    T[,] Matrix,
    CriterionDefinition<T>[] Criteria
) where T : struct, IFloatingPointIeee754<T>;
