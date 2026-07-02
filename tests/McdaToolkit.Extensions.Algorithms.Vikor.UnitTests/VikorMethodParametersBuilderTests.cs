namespace McdaToolkit.Extensions.Algorithms.Vikor.UnitTests;

public class VikorMethodParametersBuilderTests
{
    [Fact]
    public void Build_SuccesWhenValid_V()
    {
        //Arrange
        var builder = new VikorMethodParametersBuilder<double>();
        builder.WithV(0.5);
        
        //Act
        var parameter = builder.Build();

        //Assert
        Assert.Equal(0.5, parameter.V);
    }
    
    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    public void Build_FailWhenInvalidV(double invalid)
    {
        //Arrange
        var builder = new VikorMethodParametersBuilder<double>();
        builder.WithV(invalid);
        
        //Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(builder.Build);
    }
}