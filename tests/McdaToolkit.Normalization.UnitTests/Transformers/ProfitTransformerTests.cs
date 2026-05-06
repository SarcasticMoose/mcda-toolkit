using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Transformers.Types;

namespace McdaToolkit.Normalization.UnitTests.Transformers;

public class ProfitTransformerTests
{
    private readonly ProfitTransformer<double> _sut = new();

    [Fact]
    public void Transform_ReturnsSameValues()
    {
        var data = Vector<double>.Build.DenseOfArray([1.0, 3.0, 5.0]);

        var result = _sut.Transform(data);

        Assert.Equal(data.ToArray(), result.ToArray());
    }

    [Fact]
    public void Transform_DoesNotModifyData()
    {
        var data = Vector<double>.Build.DenseOfArray([10.0, 20.0, 30.0]);

        var result = _sut.Transform(data);

        for (int i = 0; i < data.Count; i++)
            Assert.Equal(data[i], result[i], tolerance: 1e-10);
    }
}
