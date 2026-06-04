# McdaToolkit

Core library of [MCDA Toolkit](https://github.com/SarcasticMoose/mcda-toolkit). Contains the domain model, pipeline infrastructure, and the base class for MCDA methods.

---

## Why a pipeline?

MCDA methods share a common structure: they receive a decision matrix, pre-process it (normalisation, transformation, weighting), and produce a ranking. Without a pipeline these concerns leak into each method implementation, making them hard to compose or reason about.

The pipeline separates **pre-processing** from **computation**. Pre-processing steps are reusable, independently testable building blocks. The method itself stays pure: it receives an already-prepared `McdaProblem<T>` and returns a `Ranking<T>`.

### Step types

| Interface | Count in pipeline | Responsibility |
|---|---|---|
| `IPreProcessingStep<T>` | 0 or more | Transforms `McdaProblem<T>` → `Result<McdaProblem<T>>` |
| `ITerminalStep<T>` | exactly 1 | Runs the MCDA method and produces `McdaSolved<T>` |

`ITerminalStep<T>` is always the last step. `PipelineBuilder<T>` enforces that only one method can be registered (it throws `InvalidOperationException` on a second `WithMethod` call).

### Error propagation

Each pre-processing step returns a `Result<McdaProblem<T>>`. `PipelineExecutor<T>` stops immediately on the first failed result and returns that failure — no subsequent steps run.


## How to build a pipeline

### 1. Define criteria

```csharp
var criteria = new[]
{
    new CriteriaBuilder<double>()
        .WithName("Cost")
        .WithType(CriterionType.Cost)
        .WithWeight(0.4)
        .Build(),

    new CriteriaBuilder<double>()
        .WithName("Quality")
        .WithType(CriterionType.Benefit)
        .WithWeight(0.6)
        .Build(),
};
```

### 2. Create an McdaProblem

```csharp
var matrix = Matrix<double>.Build.DenseOfArray(new[,]
{
    { 8.0, 7.0 },
    { 5.0, 9.0 },
    { 6.0, 6.0 },
});

var problem = new McdaProblem<double>
{
    Data = matrix,
    Criteria = criteria,
};
```

### 3. Assemble and execute the pipeline

```csharp
// INormalizationStepBuilder<double> — resolved from DI or created manually
IPreProcessingStep<double> normalizationStep = normalizationStepBuilder
    .WithMethod(NormalizationMethod.MinMax)
    .Build();

PipelineExecutor<double> executor = new PipelineBuilder<double>()
    .AddPreprocessingStep(normalizationStep)
    .WithMethod(new TopsisMethod<double>())
    .Build();

Result<McdaSolved<double>> result = executor.Execute(problem);
```

Pre-processing steps run in the order they are registered. Each step receives the output of the previous one.

## Adding a custom pre-processing step

Implement `IPreProcessingStep<T>`:

```csharp
public sealed class WeightingStep<T> : IPreProcessingStep<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public Result<McdaProblem<T>> Process(McdaProblem<T> problem)
    {
        var rows = problem.Data.RowCount;
        var cols = problem.Data.ColumnCount;
        var weighted = Matrix<T>.Build.Dense(rows, cols);

        for (int j = 0; j < cols; j++)
        {
            var weight = problem.Criteria[j].Weight;
            weighted.SetColumn(j, problem.Data.Column(j).Multiply(weight));
        }

        return Result.Success(problem with { Data = weighted });
    }
}
```

Then register it with `AddPreprocessingStep`:

```csharp
new PipelineBuilder<double>()
    .AddPreprocessingStep(new WeightingStep<double>())
    .WithMethod(new TopsisMethod<double>())
    .Build();
```
