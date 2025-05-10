using LightResults;
<<<<<<<< HEAD:src/McdaToolkit/Data/Validation/MatrixValidation/Rules/IsCriteriaDesisionBetweenMinusOneAndOneRule.cs
using McdaToolkit.Data.Validation.Abstraction;
using McdaToolkit.Data.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Data.Validation.MatrixValidation.Rules;
========
using McdaToolkit.Shared.Validation.Abstraction;
using McdaToolkit.Shared.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Shared.Validation.MatrixValidation.Rules;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Shared/Validation/MatrixValidation/Rules/IsCriteriaDesisionBetweenMinusOneAndOneRule.cs

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