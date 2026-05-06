# McdaToolkit.Normalization

Normalization module for [MCDA Toolkit](https://github.com/SarcasticMoose/mcda-toolkit). Provides vector normalization methods, cost/benefit transformers, and a pipeline step that integrates with the MCDA processing pipeline.

## Requirements

**.NET 8** or higher.

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

The typical entry point — builds a step that plugs directly into an MCDA pipeline:

```csharp
INormalizationStepBuilder<double> builder = // resolved from DI or created manually
IPreProcessingStep<double> step = builder
    .WithMethod(NormalizationMethod.MinMax)
    .Build();

Result<McdaProblem<double>> result = step.Process(problem);
```