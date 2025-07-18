using LightResults;
using McdaToolkit.Data.Validation.Abstraction;
using McdaToolkit.Data.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Data.Validation.MatrixValidation.Rules;

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