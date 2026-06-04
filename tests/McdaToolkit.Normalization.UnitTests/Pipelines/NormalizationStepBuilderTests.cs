using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Core;
using McdaToolkit.Core.Builders;
using McdaToolkit.Normalization.Normalizers;
using McdaToolkit.Normalization.Steps;
using McdaToolkit.Normalization.Transformers;
using NSubstitute;

namespace McdaToolkit.Normalization.UnitTests.Pipelines;

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
        var resolver = Substitute.For<INormalizerResolver<double>>();
        resolver.Resolve(NormalizationMethod.MinMax).Returns(new MinMaxNormalizer<double>());

        var builder = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>());
        var step = builder.Build();
        var problem = SingleBenefitProblem(1.0, 3.0, 5.0);

        step.Process(problem).IsSuccess(out var result);

        resolver.Received(1).Resolve(NormalizationMethod.MinMax);
        Assert.Equal(0.0, result!.Data[0, 0], tolerance: 1e-10);
        Assert.Equal(0.5, result.Data[1, 0], tolerance: 1e-10);
        Assert.Equal(1.0, result.Data[2, 0], tolerance: 1e-10);
    }

    [Fact]
    public void WithMethod_Max_UsesMaxNormalizer()
    {
        var resolver = Substitute.For<INormalizerResolver<double>>();
        resolver.Resolve(NormalizationMethod.Max).Returns(new MaxNormalizer<double>());

        var builder = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>());
        var step = builder.WithMethod(NormalizationMethod.Max).Build();
        var problem = SingleBenefitProblem(2.0, 4.0, 8.0);

        step.Process(problem).IsSuccess(out var result);

        resolver.Received(1).Resolve(NormalizationMethod.Max);
        Assert.Equal(0.25, result!.Data[0, 0], tolerance: 1e-10);
        Assert.Equal(0.50, result.Data[1, 0], tolerance: 1e-10);
        Assert.Equal(1.00, result.Data[2, 0], tolerance: 1e-10);
    }

    [Fact]
    public void WithMethod_ReturnsBuilderForChaining()
    {
        var resolver = Substitute.For<INormalizerResolver<double>>();
        resolver.Resolve(Arg.Any<NormalizationMethod>()).Returns(new MinMaxNormalizer<double>());

        var builder = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>());

        var returned = builder.WithMethod(NormalizationMethod.Sum);

        Assert.Same(builder, returned);
    }

    [Fact]
    public void WithMethod_CalledTwice_UsesLastMethod()
    {
        var resolver = Substitute.For<INormalizerResolver<double>>();
        resolver.Resolve(NormalizationMethod.MinMax).Returns(new MinMaxNormalizer<double>());
        resolver.Resolve(NormalizationMethod.Max).Returns(new MaxNormalizer<double>());

        var builder = new NormalizationStepBuilder<double>(resolver, new TransformerRegistry<double>());
        var step = builder
            .WithMethod(NormalizationMethod.MinMax)
            .WithMethod(NormalizationMethod.Max)
            .Build();
        var problem = SingleBenefitProblem(2.0, 4.0, 8.0);

        step.Process(problem).IsSuccess(out var result);

        Assert.Equal(0.25, result!.Data[0, 0], tolerance: 1e-10);
        Assert.Equal(1.00, result.Data[2, 0], tolerance: 1e-10);
    }
}
