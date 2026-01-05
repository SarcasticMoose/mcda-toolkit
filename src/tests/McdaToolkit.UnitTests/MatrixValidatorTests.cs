using McdaToolkit.Data.Validation.MatrixValidation;
using McdaToolkit.Data.Validation.MatrixValidation.Errors;

namespace McdaToolkit.UnitTests;

public class MatrixValidatorTests
{
    [Fact]
    public void Validate_ReturnsError_WhenMatrixIsNull()
    {
        double[,] matrix = null!;
        double[] weights = { 0.25, 0.25, 0.5 };
        int[] types = { -1, -1, 1 };

        var matrixValidator = new MatrixValidation(matrix, weights, types);
        var validationResult = matrixValidator.Validate();

        Assert.NotEmpty(validationResult.Errors);
        Assert.Contains(validationResult.Errors, e => e is NullMatrixDataError);
    }

    [Fact]
    public void Validate_ReturnsError_WhenWeightsAreNull()
    {
        double[,] matrix =
        {
            { 66, 56, 95 },
            { 61, 55, 166 },
            { 65, 49, 113 },
            { 95, 56, 99 },
            { 63, 43, 178 },
            { 74, 59, 140 },
        };
        double[] weights = null!;
        int[] types = { -1, -1, 1 };

        var matrixValidator = new MatrixValidation(matrix, weights, types);
        var validationResult = matrixValidator.Validate();

        Assert.NotEmpty(validationResult.Errors);
        Assert.Contains(validationResult.Errors, e => e is NullWeightsDataError);
    }

    [Fact]
    public void Validate_ReturnsError_WhenCriteriaDecisionsAreNull()
    {
        double[,] matrix =
        {
            { 66, 56, 95 },
            { 61, 55, 166 },
            { 65, 49, 113 },
            { 95, 56, 99 },
            { 63, 43, 178 },
            { 74, 59, 140 },
        };
        double[] weights = { 0.25, 0.25, 0.5 };
        int[] types = null!;

        var matrixValidator = new MatrixValidation(matrix, weights, types);
        var validationResult = matrixValidator.Validate();

        Assert.NotEmpty(validationResult.Errors);
        Assert.Contains(validationResult.Errors, e => e is NullCriteriaTypesDataError);
    }

    [Fact]
    public void Validate_WeightAreNotEqualOne_ShouldReturnResultFail()
    {
        double[,] matrix =
        {
            { 66, 56, 95 },
            { 61, 55, 166 },
            { 65, 49, 113 },
            { 95, 56, 99 },
            { 63, 43, 178 },
            { 74, 59, 140 },
        };
        double[] weights = { 0.8, 0.25, 0.35 };
        int[] types = { -1, -1, 1 };

        var matrixCheckerService = new MatrixValidation(matrix, weights, types);
        var result = matrixCheckerService.Validate();

        Assert.False(result.IsSuccess());
        Assert.Contains(result.Errors, e => e is WeightNotSumToOneError);
    }

    [Fact]
    public void Validate_DecisionCriteriaAreNotBetweenMinusOneAndOne_ShouldReturnResultFail()
    {
        var matrix = new double[,]
        {
            { 66, 56, 95 },
            { 61, 55, 166 },
            { 65, 49, 113 },
            { 95, 56, 99 },
            { 63, 43, 178 },
            { 74, 59, 140 },
        };
        double[] weights = { 0.4, 0.25, 0.35 };
        int[] types = { -1, -2, 1 };

        var matrixCheckerService = new MatrixValidation(matrix, weights, types);
        var result = matrixCheckerService.Validate();

        Assert.False(result.IsSuccess());
        Assert.Contains(result.Errors, e => e is DecisionCriteriaHaveIncorrectValueError);
    }

    [Fact]
    public void Validate_DimensionsOfAllMatrixesNotTheSame_ShouldReturnResultFail()
    {
        var matrix = new double[,]
        {
            { 66, 56, 95 },
            { 61, 55, 166 },
            { 65, 49, 113 },
            { 95, 56, 99 },
            { 63, 43, 178 },
            { 74, 59, 140 },
        };
        double[] weights = { 0.4, 0.25, 0.15, 0.20 };
        int[] types = { -1, -1, 1, 1 };

        var matrixCheckerService = new MatrixValidation(matrix, weights, types);
        var result = matrixCheckerService.Validate();

        Assert.False(result.IsSuccess());
        Assert.Contains(
            result.Errors,
            e => e is MatrixColumnLengthNotEqualWeightsVectorLengthError
        );
    }
}
