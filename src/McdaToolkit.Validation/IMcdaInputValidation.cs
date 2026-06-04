using System.Numerics;
using LightResults;
using McdaToolkit.Core;

namespace McdaToolkit.Validation;

/// <summary>Interface for validating MCDA input data.</summary>
public interface IMcdaInputValidation
{
    /// <summary>Validates the MCDA input data.</summary>
    /// <typeparam name="T">The numeric type of the matrix values.</typeparam>
    /// <param name="matrix">The matrix of criteria values.</param>
    /// <param name="criteria">The list of criterion definitions.</param>
    /// <returns>A result indicating the validation outcome.</returns>
    IResult Validate<T>(T[,]? matrix, List<CriterionDefinition<T>>? criteria)
        where T : struct, IFloatingPointIeee754<T>;
}
