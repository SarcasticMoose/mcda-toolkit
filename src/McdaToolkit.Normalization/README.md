# McdaToolkit.Normalization

Normalization module for [MCDA Toolkit](https://github.com/SarcasticMoose/mcda-toolkit). Provides vector normalization methods, cost/benefit transformers, and a pipeline step that integrates with the MCDA processing pipeline.

## Available Normalization Methods

| Method | Enum value | Formula |
|---|---|---|
| Min-Max | `NormalizationMethod.MinMax` | `(x - min) / (max - min)` |
| Max | `NormalizationMethod.Max` | `x / max` |
| Sum | `NormalizationMethod.Sum` | `x / Σx` |
| Vector (L2) | `NormalizationMethod.Vector` | `x / √(Σx²)` |
| Logarithmic | `NormalizationMethod.Logarithmic` | `ln(x) / ln(Πx)` |

## Criterion Transformation

Before normalization, each column is transformed based on its criterion type:

- **Benefit** — values are passed through unchanged.
- **Cost** — values are inverted using `max - x`, so higher cost becomes lower score.

The transformation is applied automatically when the normalization step processes an `McdaProblem<T>`.

## Usage

### As a pipeline step

`IPreProcessingStep<T>` is a building block of an MCDA pipeline. The step is registered via the builder and executed as part of the pipeline during processing.

> [!IMPORTANT]
> The normalization step is **optional** — if the input data has already been normalized upstream, this step can be skipped. The order of steps in the pipeline matters: normalization must be applied before any method that assumes normalized input.

```csharp
INormalizationStepBuilder<double> builder = // resolved from DI or created manually
IPreProcessingStep<double> step = builder
    .WithMethod(NormalizationMethod.MinMax)
    .Build();
```