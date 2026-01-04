using LightResults;

namespace McdaToolkit.Data.Validation.Abstraction;

/// <inheritdoc cref="IValidationService"/>
internal abstract class ValidationServiceBase : IValidationService
{
    protected ICollection<IValidationRule> Rules { get; } = new List<IValidationRule>();
    
    /// <inheritdoc cref="IValidationService.Validate"/>
    public IResult Validate()
    {
        var rulesResult = new List<IResult>();
        foreach (IValidationRule rule in Rules)
        {
            var ruleResult = rule.IsValid();
            rulesResult.Add(ruleResult);
            
            if(ruleResult.IsFailure()) break;
        }

        var rulesErrors = rulesResult
            .SelectMany(x => x.Errors)
            .ToArray();
        
        return rulesErrors.Length > 0 ? Result.Failure(rulesErrors) : Result.Success();
    }
}