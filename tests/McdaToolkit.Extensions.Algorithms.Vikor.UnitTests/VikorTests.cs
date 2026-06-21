using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Core;
using McdaToolkit.Core.Builders;
using McdaToolkit.Normalization;
using McdaToolkit.Normalization.Steps;
using McdaToolkit.Pipeline;

namespace McdaToolkit.Extensions.Algorithms.Vikor.UnitTests;

public class VikorTests
{
    [Fact]
    public void Tests()
    {
        var matrix = new[,]
        {
            { 0.297044, 0.450988, 0.289430, 0.439375, 0.504433, 0.378148 },
            { 0.396059, 0.300658, 0.434145, 0.659062, 0.403547, 0.0180071 },
            { 0.594089, 0.375823, 0.651217, 0.527250, 0.000000, 0.000000 },
            { 0.495074, 0.450988, 0.217072, 0.307562, 0.100887, 0.738289 },
            { 0.396059, 0.601317, 0.506502, 0.000000, 0.756650, 0.558219 },
        };

        var problem = new McdaProblemBuilder<double>()
            .WithMatrix(matrix)
            .AddCriterion(c =>
            {
                c.WithName("Weight 1");
                c.WithType(CriterionType.Benefit);
                c.WithWeight(0.1);
            })
            .AddCriterion(c =>
            {
                c.WithName("Weight 2");
                c.WithType(CriterionType.Benefit);
                c.WithWeight(0.2);
            })
            .AddCriterion(c =>
            {
                c.WithName("Weight 3");
                c.WithType(CriterionType.Benefit);
                c.WithWeight(0.1);
            })
            .AddCriterion(c =>
            {
                c.WithName("Weight 4");
                c.WithType(CriterionType.Cost);
                c.WithWeight(0.2);
            })
            .AddCriterion(c =>
            {
                c.WithName("Weight 5");
                c.WithType(CriterionType.Cost);
                c.WithWeight(0.1);
            })
            .AddCriterion(c =>
            {
                c.WithName("Weight 6");
                c.WithType(CriterionType.Cost);
                c.WithWeight(0.3);
            })
            .Build();

        var parameters = new VikorMethodParametersBuilder<double>()
            .WithV(0.5)
            .Build();
        
        var vikor = new VikorMethod<double>(parameters);
        var result = vikor.Execute(problem);
        
        Assert.Equal(0.25, result.DQ);
        Assert.Null(result.BestAlternativeIndex);
        Assert.Collection(
            result.CompromiseSetIndices,
            i => Assert.Equal(4, i),
            i => Assert.Equal(5, i));
    }
}
