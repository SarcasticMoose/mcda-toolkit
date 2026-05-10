using McdaToolkit.Normalization.Abstractions;
using McdaToolkit.Normalization.Normalizers;
using McdaToolkit.Normalization.Transformers;

namespace McdaToolkit.Normalization.UnitTests;

public class VectorNormalizationPipelineTests
{
    [Fact]
    public void Process_Benefit_AppliesNormalizationOnly()
    {
        var pipeline = new VectorNormalizationPipeline<double>(
            new ProfitTransformer<double>(),
            new MaxNormalizer<double>());
        var data = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray([2.0, 4.0, 8.0]);

        var result = pipeline.Process(data);

        Assert.Equal(0.25, result[0], tolerance: 1e-10);
        Assert.Equal(0.50, result[1], tolerance: 1e-10);
        Assert.Equal(1.00, result[2], tolerance: 1e-10);
    }

    [Fact]
    public void Process_Cost_AppliesTransformThenNormalization()
    {
        // [1, 3, 5] → CostTransform max=5 → [4, 2, 0] → MaxNorm max=4 → [1, 0.5, 0]
        var pipeline = new VectorNormalizationPipeline<double>(
            new CostTransformer<double>(),
            new MaxNormalizer<double>());
        var data = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray([1.0, 3.0, 5.0]);

        var result = pipeline.Process(data);

        Assert.Equal(1.0, result[0], tolerance: 1e-10);
        Assert.Equal(0.5, result[1], tolerance: 1e-10);
        Assert.Equal(0.0, result[2], tolerance: 1e-10);
    }

    [Fact]
    public void Process_Cost_WithMinMax_InvertsOrdering()
    {
        // [1, 3, 5] → CostTransform max=5 → [4, 2, 0] → MinMax → [1, 0.5, 0]
        var pipeline = new VectorNormalizationPipeline<double>(
            new CostTransformer<double>(),
            new MinMaxNormalizer<double>());
        var data = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray([1.0, 3.0, 5.0]);

        var result = pipeline.Process(data);

        Assert.Equal(1.0, result[0], tolerance: 1e-10);
        Assert.Equal(0.5, result[1], tolerance: 1e-10);
        Assert.Equal(0.0, result[2], tolerance: 1e-10);
    }
}