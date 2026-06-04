using System.Numerics;
using LightResults;
using McdaToolkit.Validation.Context;

namespace McdaToolkit.Validation.Rules;

internal sealed class ValidationPipeline<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly List<IValidationRule<RawMcdaInput<T>>>   _structuralRules = [];
    private readonly List<IValidationRule<ValidMcdaInput<T>>> _businessRules   = [];

    public ValidationPipeline<T> AddStructuralRule(IValidationRule<RawMcdaInput<T>> rule)
    {
        _structuralRules.Add(rule);
        return this;
    }

    public ValidationPipeline<T> AddBusinessRule(IValidationRule<ValidMcdaInput<T>> rule)
    {
        _businessRules.Add(rule);
        return this;
    }

    public IResult Validate(T[,]? matrix, List<CriterionDefinition<T>>? criteria)
    {
        var raw = new RawMcdaInput<T>(matrix, criteria);

        var structuralErrors = _structuralRules
            .Select(r => r.IsValid(raw))
            .Where(r => r.IsFailure())
            .SelectMany(r => r.Errors)
            .ToArray();

        if (structuralErrors.Length > 0)
            return Result.Failure(structuralErrors);

        var valid = new ValidMcdaInput<T>(matrix!, criteria!.ToArray());

        var businessErrors = _businessRules
            .Select(r => r.IsValid(valid))
            .Where(r => r.IsFailure())
            .SelectMany(r => r.Errors)
            .ToArray();

        return businessErrors.Length > 0 ? Result.Failure(businessErrors) : Result.Success();
    }
}
