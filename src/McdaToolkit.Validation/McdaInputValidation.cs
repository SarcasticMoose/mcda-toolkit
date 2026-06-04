using System.Numerics;
using LightResults;
using McdaToolkit.Core;
using McdaToolkit.Validation.Rules;

namespace McdaToolkit.Validation;

internal sealed class McdaInputValidation : IMcdaInputValidation
{
    public IResult Validate<T>(T[,]? matrix, List<CriterionDefinition<T>>? criteria)
        where T : struct, IFloatingPointIeee754<T>
        => new ValidationPipeline<T>()
            .AddStructuralRule(new SupportedNumericTypeRule<T>())
            .AddStructuralRule(new NotNullRule<T>())
            .AddBusinessRule(new WeightsSumToOneRule<T>())
            .AddBusinessRule(new DimensionsMatchRule<T>())
            .Validate(matrix, criteria);
}
