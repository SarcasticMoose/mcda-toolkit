using LightResults;
using McdaToolkit.Validation.Abstraction;
using McdaToolkit.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Validation.MatrixValidation.Rules;

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
            return Result.Fail(new DecisionCriteriaHaveIncorrectValueError());
        }
        return Result.Ok();
    }
}