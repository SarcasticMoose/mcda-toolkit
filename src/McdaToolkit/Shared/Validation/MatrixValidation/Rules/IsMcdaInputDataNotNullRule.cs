using LightResults;
using McdaToolkit.Shared.Providers.Errors;
using McdaToolkit.Shared.Validation.Abstraction;

namespace McdaToolkit.Shared.Validation.MatrixValidation.Rules;

internal class IsMcdaInputDataNotNullRule : IValidationRule
{
#nullable disable
    
    private readonly double[,] _matrix;
    private readonly double[] _weights;
    private readonly int[] _criteriaTypes;

    public IsMcdaInputDataNotNullRule(double[,] matrix, double[] weights, int[] criteriaTypes)
    {
        _matrix = matrix;
        _weights = weights;
        _criteriaTypes = criteriaTypes;
    }
    
    public IResult IsValid()
    {
        ICollection<IError> listOfErrors = new List<IError>();    
        if (_matrix is null)
        {
            listOfErrors.Add(new NullMatrixDataError());
        }

        if (_weights is null)
        {
            listOfErrors.Add(new NullWeightsDataError());
        }

        if (_criteriaTypes is null)
        {
            listOfErrors.Add(new NullCriteriaTypesDataError());
        }

        return listOfErrors.Count > 0 ? Result.Fail(listOfErrors) : Result.Ok();
    }
}