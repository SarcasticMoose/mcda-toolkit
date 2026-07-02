# MCDA Toolkit

The MCDA Toolkit is a flexible .NET library designed to support Multi-Criteria Decision Analysis (MCDA) processes. It assists developers and analysts in structuring, evaluating, and solving decision-making problems involving multiple, often conflicting, criteria. The toolkit provides a streamlined interface for implementing various MCDA methods, facilitating informed and transparent decision-making
## Getting started

```bash
dotnet add package McdaToolkit
```

## Extensions

The core package is intentionally minimal and does not include all available features.
Additional functionality (such as MCDA methods, pipeline steps etc.) is provided via separate NuGet packages:

```
McdaToolkit.Extensions.*
```

Install only the extensions you need for your use case.

Example:
```bash
dotnet add package McdaToolkit.Extensions.Vikor
```

## Example usage (VIKOR)

Below is an example of a complete MCDA pipeline using the VIKOR method:

```csharp
var pipelineExecutor = new PipelineBuilder<double>()
    .ApplyData(data =>
    {
        data.WithMatrix(new[,]
        {
            { 690, 10.1 },
            { 590, 3.9 },
            { 600, 3.6 },
            { 620, 3.8 },
            { 700, 2.8 },
            { 650, 4.0 },
        });
        data.AddCriterion(criterion =>
        {
            criterion.WithWeight(0.2);
            criterion.WithType(CriterionType.Benefit);
            criterion.WithName("Weight 1");
        });
        data.AddCriterion(criterion =>
        {
            criterion.WithWeight(0.80);
            criterion.WithType(CriterionType.Benefit);
            criterion.WithName("Weight 2");
        });
    })
    .AddNormalizationStep(configure => configure.WithMethod(NormalizationMethod.Vector))
    .Build();

var vikor = new VikorMethodBuilder<double>()
    .WithParameters(parameters => parameters.WithV(0.1))
    .Build();

if (!pipelineExecutor.IsSuccess(out var executor)) return;
var ranking = executor.Execute(vikor)
if (!ranking.IsSuccess(out var solved)) return;

```
## Next steps

See the full [documentation](https://sarcasticmoose.github.io/mcda-toolkit) for more details