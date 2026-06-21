using System;
using System.Text.Json;
using McdaToolkit.Extensions.Algorithms.Vikor;
using McdaToolkit.Core;
using McdaToolkit.Normalization;
using McdaToolkit.Normalization.Steps;
using McdaToolkit.Pipeline;
var matrix = new[,]
{
    { 3, 6, 4, 20, 2, 30000 },
    { 4, 4, 6, 15, 2.2, 32000 },
    { 6, 5, 9, 18, 3, 32100 },
    { 5, 6, 3, 23, 2.8, 28000 },
    { 4, 8, 7, 30, 1.5, 29000 },
};
var pipelineExecutor = new PipelineBuilder<double>()
    .ApplyData(problem =>
    {
        problem.WithMatrix(matrix);
        problem.AddCriterion(c =>
        {
            c.WithName("Weight 1");
            c.WithType(CriterionType.Benefit);
            c.WithWeight(0.1);
        });
        problem.AddCriterion(c =>
        {
            c.WithName("Weight 2");
            c.WithType(CriterionType.Benefit);
            c.WithWeight(0.2);
        });
        problem.AddCriterion(c =>
        {
            c.WithName("Weight 3");
            c.WithType(CriterionType.Benefit);
            c.WithWeight(0.1);
        });
        problem.AddCriterion(c =>
        {
            c.WithName("Weight 4");
            c.WithType(CriterionType.Cost);
            c.WithWeight(0.2);
        });
        problem.AddCriterion(c =>
        {
            c.WithName("Weight 5");
            c.WithType(CriterionType.Cost);
            c.WithWeight(0.1);
        });
        problem.AddCriterion(c =>
        {
            c.WithName("Weight 6");
            c.WithType(CriterionType.Cost);
            c.WithWeight(0.3);
        });
    })
    .AddNormalizationStep(configure => configure.WithMethod(NormalizationMethod.Vector))
    .Build();

var vikor = new VikorMethodBuilder<double>()
    .WithParameters(parameters => parameters.WithV(0.5))
    .Build();

if (!pipelineExecutor.IsSuccess(out var executor)) return;
var ranking = executor.Execute(vikor);
if (!ranking.IsSuccess(out var solved)) return;
Console.WriteLine(JsonSerializer.Serialize(solved, new JsonSerializerOptions()
{
    WriteIndented = true
}));
