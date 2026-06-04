using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Core;
using McdaToolkit.Core.Builders;
using McdaToolkit.Normalization.Normalizers;
using McdaToolkit.Normalization.Steps;
using McdaToolkit.Normalization.Transformers;

namespace McdaToolkit.Normalization.UnitTests;

public class NormalizationStepTests
{
    private static McdaProblem<double> BuildProblem(double[,] values, params CriterionType[] types)
    {
        var matrix = Matrix<double>.Build.DenseOfArray(values);
        var criteria = types
            .Select(t => new CriteriaBuilder<double>().WithType(t).Build())
            .ToArray();
        return new McdaProblem<double> { Data = matrix, Criteria = criteria };
    }

    [Fact]
    public void Process_BenefitColumn_WithMinMax_NormalizesCorrectly()
    {
        // [[1], [3], [5]] → Profit pass-through → MinMax → [[0], [0.5], [1]]
        var step = new NormalizationStep<double>(
            new MinMaxNormalizer<double>(),
            new TransformerRegistry<double>());
        var problem = BuildProblem(new[,] { { 1.0 }, { 3.0 }, { 5.0 } }, CriterionType.Benefit);

        step.Process(problem).IsSuccess(out var result);

        Assert.Equal(0.0, result!.Data[0, 0], tolerance: 1e-10);
        Assert.Equal(0.5, result.Data[1, 0], tolerance: 1e-10);
        Assert.Equal(1.0, result.Data[2, 0], tolerance: 1e-10);
    }

    [Fact]
    public void Process_CostColumn_WithMinMax_TransformsAndNormalizes()
    {
        // [8, 4, 2] → CostTransform max=8 → [0, 4, 6] → MinMax → [0, 2/3, 1]
        var step = new NormalizationStep<double>(
            new MinMaxNormalizer<double>(),
            new TransformerRegistry<double>());
        var problem = BuildProblem(new[,] { { 8.0 }, { 4.0 }, { 2.0 } }, CriterionType.Cost);

        step.Process(problem).IsSuccess(out var result);

        Assert.Equal(0.0, result!.Data[0, 0], tolerance: 1e-10);
        Assert.Equal(4.0 / 6.0, result.Data[1, 0], tolerance: 1e-10);
        Assert.Equal(1.0, result.Data[2, 0], tolerance: 1e-10);
    }

    [Fact]
    public void Process_MultipleColumns_ProcessesEachColumnIndependently()
    {
        // Col 0 (Benefit, MinMax): [1, 3, 5] → [0, 0.5, 1]
        // Col 1 (Cost,    MinMax): [8, 4, 2] → transform [0, 4, 6] → [0, 2/3, 1]
        var step = new NormalizationStep<double>(
            new MinMaxNormalizer<double>(),
            new TransformerRegistry<double>());

        var problem = BuildProblem(
            new[,] { { 1.0, 8.0 }, { 3.0, 4.0 }, { 5.0, 2.0 } },
            CriterionType.Benefit,
            CriterionType.Cost);

        step.Process(problem).IsSuccess(out var result);

        Assert.Equal(0.0, result!.Data[0, 0], tolerance: 1e-10);
        Assert.Equal(0.5, result.Data[1, 0], tolerance: 1e-10);
        Assert.Equal(1.0, result.Data[2, 0], tolerance: 1e-10);

        Assert.Equal(0.0, result.Data[0, 1], tolerance: 1e-10);
        Assert.Equal(4.0 / 6.0, result.Data[1, 1], tolerance: 1e-10);
        Assert.Equal(1.0, result.Data[2, 1], tolerance: 1e-10);
    }

    [Fact]
    public void Process_WithMaxNormalizer_BenefitColumn()
    {
        // [2, 4, 8] → MaxNorm → [0.25, 0.5, 1]
        var step = new NormalizationStep<double>(
            new MaxNormalizer<double>(),
            new TransformerRegistry<double>());
        var problem = BuildProblem(new[,] { { 2.0 }, { 4.0 }, { 8.0 } }, CriterionType.Benefit);

        step.Process(problem).IsSuccess(out var result);

        Assert.Equal(0.25, result!.Data[0, 0], tolerance: 1e-10);
        Assert.Equal(0.50, result.Data[1, 0], tolerance: 1e-10);
        Assert.Equal(1.00, result.Data[2, 0], tolerance: 1e-10);
    }

    [Fact]
    public void Process_ReturnsSuccess()
    {
        var step = new NormalizationStep<double>(
            new MaxNormalizer<double>(),
            new TransformerRegistry<double>());
        var problem = BuildProblem(new[,] { { 2.0 }, { 4.0 }, { 8.0 } }, CriterionType.Benefit);

        var result = step.Process(problem);

        Assert.True(result.IsSuccess(out _));
    }

    [Fact]
    public void Process_PreservesMatrixDimensions()
    {
        var step = new NormalizationStep<double>(
            new SumNormalizer<double>(),
            new TransformerRegistry<double>());
        var problem = BuildProblem(
            new[,] { { 1.0, 2.0 }, { 3.0, 4.0 }, { 5.0, 6.0 } },
            CriterionType.Benefit,
            CriterionType.Benefit);

        step.Process(problem).IsSuccess(out var result);

        Assert.Equal(3, result!.Data.RowCount);
        Assert.Equal(2, result.Data.ColumnCount);
    }
}
