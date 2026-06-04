using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Builders;
using McdaToolkit.Normalization.Steps;
using McdaToolkit.Normalization.Transformers;

namespace McdaToolkit.Normalization.UnitTests;

public class NormalizationStepBuilderTests
{
    private static McdaProblem<double> SingleBenefitProblem(params double[] values)
    {
        var matrix = Matrix<double>.Build.DenseOfColumnArrays(values);
        var criteria = new[] { new CriteriaBuilder<double>().WithType(CriterionType.Benefit).Build() };
        return new McdaProblem<double> { Data = matrix, Criteria = criteria };
    }

    [Fact]
    public void Build_WithoutWithMethod_DefaultsToMinMax()
    {
        var resolver = new TestNormalizerResolver<double>();
        var builder = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>());

        builder.Build();

        Assert.Equal(NormalizationMethod.MinMax, resolver.LastResolvedMethod);
    }

    [Fact]
    public void Build_WithoutWithMethod_ProducesMinMaxBehavior()
    {
        // [1, 3, 5] → MinMax → [0, 0.5, 1]
        var resolver = new TestNormalizerResolver<double>();
        var step = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>()).Build();

        step.Process(SingleBenefitProblem(1.0, 3.0, 5.0)).IsSuccess(out var result);

        Assert.Equal(0.0, result!.Data[0, 0], tolerance: 1e-10);
        Assert.Equal(0.5, result.Data[1, 0], tolerance: 1e-10);
        Assert.Equal(1.0, result.Data[2, 0], tolerance: 1e-10);
    }

    [Fact]
    public void WithMethod_Max_ResolvesMaxMethod()
    {
        var resolver = new TestNormalizerResolver<double>();
        var builder = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>());

        builder.WithMethod(NormalizationMethod.Max).Build();

        Assert.Equal(NormalizationMethod.Max, resolver.LastResolvedMethod);
    }

    [Fact]
    public void WithMethod_Max_ProducesMaxBehavior()
    {
        // [2, 4, 8] → MaxNorm → [0.25, 0.5, 1]
        var resolver = new TestNormalizerResolver<double>();
        var step = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>())
            .WithMethod(NormalizationMethod.Max)
            .Build();

        step.Process(SingleBenefitProblem(2.0, 4.0, 8.0)).IsSuccess(out var result);

        Assert.Equal(0.25, result!.Data[0, 0], tolerance: 1e-10);
        Assert.Equal(0.50, result.Data[1, 0], tolerance: 1e-10);
        Assert.Equal(1.00, result.Data[2, 0], tolerance: 1e-10);
    }

    [Fact]
    public void WithMethod_ReturnsBuilderForChaining()
    {
        var resolver = new TestNormalizerResolver<double>();
        var builder = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>());

        var returned = builder.WithMethod(NormalizationMethod.Sum);

        Assert.Same(builder, returned);
    }

    [Fact]
    public void WithMethod_CalledTwice_UsesLastMethod()
    {
        var resolver = new TestNormalizerResolver<double>();
        var builder = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>());

        builder.WithMethod(NormalizationMethod.MinMax).WithMethod(NormalizationMethod.Max).Build();

        Assert.Equal(NormalizationMethod.Max, resolver.LastResolvedMethod);
    }
}
