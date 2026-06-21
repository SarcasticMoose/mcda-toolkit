using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Core.Builders;

namespace McdaToolkit.Core.UnitTests;

public class McdaProblemBuilderExtensionsTests
{
    [Fact]
    public void WithMatrix_FromRows_ShouldSetData()
    {
        // Arrange
        var builder = new McdaProblemBuilder<double>();

        var rows = new[]
        {
            new[] { 1.0, 2.0 },
            new[] { 3.0, 4.0 },
        };

        // Act
        builder.WithMatrix(rows);
        var problem = builder.Build();

        // Assert
        Assert.Equal(2, problem.Data.RowCount);
        Assert.Equal(2, problem.Data.ColumnCount);

        Assert.Equal(1.0, problem.Data[0, 0]);
        Assert.Equal(2.0, problem.Data[0, 1]);
        Assert.Equal(3.0, problem.Data[1, 0]);
        Assert.Equal(4.0, problem.Data[1, 1]);
    }

    [Fact]
    public void WithMatrix_FromMathNetMatrix_ShouldSetData()
    {
        // Arrange
        var builder = new McdaProblemBuilder<double>();

        var matrix = Matrix<double>.Build.DenseOfArray(new[,]
        {
            { 1.0, 2.0 },
            { 3.0, 4.0 },
        });

        // Act
        builder.WithMatrix(matrix);
        var problem = builder.Build();

        // Assert
        Assert.Equal(2, problem.Data.RowCount);
        Assert.Equal(2, problem.Data.ColumnCount);

        Assert.Equal(1.0, problem.Data[0, 0]);
        Assert.Equal(2.0, problem.Data[0, 1]);
        Assert.Equal(3.0, problem.Data[1, 0]);
        Assert.Equal(4.0, problem.Data[1, 1]);
    }
}