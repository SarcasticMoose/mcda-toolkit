using LightResults;

namespace McdaToolkit.Validation.Abstraction;

public abstract class ValidationServiceBase : IValidationService
{
    protected ICollection<IValidationRule> Rules { get; } = new List<IValidationRule>();
    
    public IResult Validate()
    {
        var rulesResult = new List<IResult>();
        foreach (IValidationRule rule in Rules)
        {
            var ruleResult = rule.IsValid();
            rulesResult.Add(ruleResult);
            if (ruleResult.IsFailed)
            {
                break;
            }
        }

        var rulesErrors = rulesResult
            .SelectMany(x => x.Errors)
            .ToArray();
        
        return rulesErrors.Length > 0 ? Result.Fail(rulesErrors) : Result.Ok();
    }
}