using McdaToolkit.Builders;
using McdaToolkit.Validation;

namespace McdaToolkit.Validation.UnitTests;

public sealed class McdaInputValidationTests
{
    private static List<CriterionDefinition<double>> BuildCriteria(params double[] weights)
        => weights.Select(w => new CriteriaBuilder<double>()
            .WithType(CriterionType.Benefit)
            .WithWeight(w)
            .Build()).ToList();

    [Fact]
    public void Validate_ReturnsSuccess_WhenInputIsValid()
    {
        double[,] matrix = { { 1, 2 }, { 3, 4 } };
        var criteria = BuildCriteria(0.5, 0.5);

        var result = McdaInputValidation.Validate(matrix, criteria);

        Assert.True(result.IsSuccess());
    }

    [Fact]
    public void Validate_ReturnsFailure_WhenMatrixIsNull()
    {
        var criteria = BuildCriteria(0.5, 0.5);

        var result = McdaInputValidation.Validate<double>(null, criteria);

        Assert.True(result.IsFailure());
    }

    [Fact]
    public void Validate_ReturnsFailure_WhenCriteriaIsNull()
    {
        double[,] matrix = { { 1, 2 }, { 3, 4 } };

        var result = McdaInputValidation.Validate<double>(matrix, null);

        Assert.True(result.IsFailure());
    }

    [Fact]
    public void Validate_ReturnsBothErrors_WhenMatrixAndCriteriaAreNull()
    {
        var result = McdaInputValidation.Validate<double>(null, null);

        Assert.True(result.IsFailure());
        Assert.Equal(2, result.Errors.Count);
    }

    [Fact]
    public void Validate_ReturnsFailure_WhenWeightsDoNotSumToOne()
    {
        double[,] matrix = { { 1, 2 }, { 3, 4 } };
        var criteria = BuildCriteria(0.3, 0.3);

        var result = McdaInputValidation.Validate(matrix, criteria);

        Assert.True(result.IsFailure());
    }

    [Fact]
    public void Validate_ReturnsFailure_WhenColumnCountDoesNotMatchCriteriaCount()
    {
        double[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };
        var criteria = BuildCriteria(0.5, 0.5);

        var result = McdaInputValidation.Validate(matrix, criteria);

        Assert.True(result.IsFailure());
    }

    [Fact]
    public void Validate_ReturnsFailure_WhenTypeIsUnsupported()
    {
        Half[,] matrix = { { (Half)1, (Half)2 }, { (Half)3, (Half)4 } };
        var criteria = new List<CriterionDefinition<Half>>
        {
            new CriteriaBuilder<Half>().WithType(CriterionType.Benefit).WithWeight((Half)0.5).Build(),
            new CriteriaBuilder<Half>().WithType(CriterionType.Benefit).WithWeight((Half)0.5).Build(),
        };

        var result = McdaInputValidation.Validate(matrix, criteria);

        Assert.True(result.IsFailure());
    }

    [Fact]
    public void Validate_ReturnsSuccess_WhenTypeIsFloat()
    {
        float[,] matrix = { { 1f, 2f }, { 3f, 4f } };
        var criteria = new List<CriterionDefinition<float>>
        {
            new CriteriaBuilder<float>().WithType(CriterionType.Benefit).WithWeight(0.5f).Build(),
            new CriteriaBuilder<float>().WithType(CriterionType.Benefit).WithWeight(0.5f).Build(),
        };

        var result = McdaInputValidation.Validate(matrix, criteria);

        Assert.True(result.IsSuccess());
    }
}
