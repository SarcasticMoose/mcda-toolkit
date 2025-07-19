using LightResults;
using McdaToolkit.Shared.Validation.Abstraction;
using McdaToolkit.Shared.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Shared.Validation.MatrixValidation.Rules;

public class IsCriteriaDesisionBetweenMinusOneAndOneRule : IValidationRule
{
    private readonly int[] _criteriaDecision;

    public IsCriteriaDesisionBetweenMinusOneAndOneRule(int[] criteriaDecision)
    {
        _criteriaDecision = criteriaDecision;
    }
    public IResult IsValid()
    {
        var isCriteriaDecisionCorrect = _criteriaDecision.All(n => n is >= -1 and <= 1);
        if (!isCriteriaDecisionCorrect)
        {
            return Result.Failure(new DecisionCriteriaHaveIncorrectValueError());
        }
        return Result.Success();
    }
}