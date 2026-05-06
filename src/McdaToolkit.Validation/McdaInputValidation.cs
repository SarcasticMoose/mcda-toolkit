using System.Numerics;
using LightResults;
using McdaToolkit.Models.Mcda;
using McdaToolkit.Validation.Abstraction;
using McdaToolkit.Validation.MatrixValidation.Rules;

namespace McdaToolkit.Validation.MatrixValidation;

public static class McdaInputValidation
{
    public static IResult Validate<T>(T[,]? matrix, List<CriterionDefinition<T>>? criteria)
        where T : struct, IFloatingPointIeee754<T>
        => new ValidationPipeline<T>()
            .AddStructuralRule(new SupportedNumericTypeRule<T>())
            .AddStructuralRule(new NotNullRule<T>())
            .AddBusinessRule(new WeightsSumToOneRule<T>())
            .AddBusinessRule(new DimensionsMatchRule<T>())
            .Validate(matrix, criteria);
}
