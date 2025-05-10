#nullable disable
using LightResults;
<<<<<<< HEAD
=======
<<<<<<<< HEAD:src/McdaToolkit/Data/Validation/MatrixValidation/Rules/IsDataWeightsAndTypesHaveCorrectSizesRule.cs
using McdaToolkit.Data.Validation.Abstraction;
using McdaToolkit.Data.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Data.Validation.MatrixValidation.Rules;
========
>>>>>>> cc9253a (feat: updated namespaces)
using McdaToolkit.Shared.Validation.Abstraction;
using McdaToolkit.Shared.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Shared.Validation.MatrixValidation.Rules;
<<<<<<< HEAD
=======
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Shared/Validation/MatrixValidation/Rules/IsDataWeightsAndTypesHaveCorrectSizesRule.cs
>>>>>>> cc9253a (feat: updated namespaces)

public class IsDataWeightsAndTypesHaveCorrectSizesRule : IValidationRule
{
    private readonly double[,] _matrix;
    private readonly double[] _weights;
    private readonly int[] _criteriaDecision;

    public IsDataWeightsAndTypesHaveCorrectSizesRule(double[,] matrix,double[] weights,int[] criteriaDecision)
    {
        _matrix = matrix;
        _weights = weights;
        _criteriaDecision = criteriaDecision;
    }
    public IResult IsValid()
    {
        var matrixColumns = _matrix.GetLength(1);
        var isCorrectSizes = matrixColumns == _weights.Length && matrixColumns == _criteriaDecision.Length;
        if (!isCorrectSizes)
        {
            return Result.Failure(new MatrixColumnLengthNotEqualWeightsVectorLengthError());
        }
        return Result.Success();
    }
}