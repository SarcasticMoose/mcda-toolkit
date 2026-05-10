using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Transformers;

namespace McdaToolkit.Normalization.UnitTests.Transformers;

public class TransformerRegistryTests
{
    private readonly TransformerRegistry<double> _sut = new();

    [Fact]
    public void Get_Benefit_ReturnsProfitTransformerBehavior()
    {
        var transformer = _sut.Get(CriterionType.Benefit);
        var data = Vector<double>.Build.DenseOfArray([1.0, 3.0, 5.0]);

        var result = transformer.Transform(data);

        for (int i = 0; i < data.Count; i++)
            Assert.Equal(data[i], result[i], tolerance: 1e-10);
    }

    [Fact]
    public void Get_Cost_ReturnsCostTransformerBehavior()
    {
        var transformer = _sut.Get(CriterionType.Cost);
        var data = Vector<double>.Build.DenseOfArray([1.0, 3.0, 5.0]);

        var result = transformer.Transform(data);

        // max=5, so [5-1, 5-3, 5-5] = [4, 2, 0]
        Assert.Equal(4.0, result[0], tolerance: 1e-10);
        Assert.Equal(2.0, result[1], tolerance: 1e-10);
        Assert.Equal(0.0, result[2], tolerance: 1e-10);
    }

    [Fact]
    public void Get_Benefit_AndCost_ReturnDifferentTransformers()
    {
        var profit = _sut.Get(CriterionType.Benefit);
        var cost = _sut.Get(CriterionType.Cost);

        Assert.IsNotType(profit.GetType(), cost);
    }

    [Fact]
    public void Get_InvalidType_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Get((CriterionType)999));
    }
}
