<<<<<<<< HEAD:src/McdaToolkit/Data/Validation/MatrixValidation/MatrixValidation.cs
﻿using McdaToolkit.Data.Validation.Abstraction;
using McdaToolkit.Data.Validation.MatrixValidation.Rules;

namespace McdaToolkit.Data.Validation.MatrixValidation;
========
﻿using McdaToolkit.Shared.Validation.Abstraction;
using McdaToolkit.Shared.Validation.MatrixValidation.Rules;

namespace McdaToolkit.Shared.Validation.MatrixValidation;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Shared/Validation/MatrixValidation/MatrixValidation.cs

internal sealed class MatrixValidation : ValidationServiceBase
{
    public MatrixValidation(double[,] matrix, double[] weights, int[] criteriaTypes)
    {
        Rules.Add(new IsMcdaInputDataNotNullRule(matrix, weights, criteriaTypes));
        Rules.Add(new IsWeightsEqualOneRule(weights));
        Rules.Add(new IsCriteriaDesisionBetweenMinusOneAndOneRule(criteriaTypes));
        Rules.Add(new IsDataWeightsAndTypesHaveCorrectSizesRule(matrix,weights,criteriaTypes));
    }
}