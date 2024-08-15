using FluentAssertions;
using McdaToolkit.Mcda.Helpers.Errors;
using McdaToolkit.Mcda.Services;

namespace McdaToolkit.UnitTests;

public class MatrixCheckerTests
{
    [Fact]
    public void ValidateData_WeightAreNotEqualOne_ShouldReturnResultFail()
    {
        double[,] matrix = {
            { 66, 56, 95 },
            { 61, 55, 166 },
            { 65, 49, 113 },
            { 95, 56, 99 },
            { 63, 43, 178 },
            { 74, 59, 140 },
        };
        double[] weights = [0.8,0.25,0.35];
        int[] types = [-1, -1, 1];
        var matrixCheckerService = new MatrixCheckerService();

        var result = matrixCheckerService.ValidateData(matrix, weights, types);

        result.IsSuccess.Should().BeFalse();
        result.HasError<WeightNotSumToOneError>();
    }
    
    [Fact]
    public void Calculate_DecisionCriteriaAreNotBetweenMinusOneAndOne_ShouldReturnResultFail()
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
        double[] weights = [0.4,0.25,0.35];
        int[] types = [-1, -2, 1];
        var matrixCheckerService = new MatrixCheckerService();
        
        var result = matrixCheckerService.ValidateData(matrix, weights, types);
        
        result.IsSuccess.Should().BeFalse();
        result.HasError<DecisionCriteriaHaveIncorrectValueError>();
    }
    
    [Fact]
    public void Calculate_DimensionsOfAllMatrixesNotTheSame_ShouldReturnResultFail()
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
        double[] weights = [0.4,0.25,0.15,0.20];
        int[] types = [-1, -1, 1, 1];
        var matrixCheckerService = new MatrixCheckerService();
        
        var result = matrixCheckerService.ValidateData(matrix, weights, types);

        result.IsSuccess.Should().BeFalse();
        result.HasError<MatrixColumnLengthNotEqualWeightsVectorLengthError>();
    }
}