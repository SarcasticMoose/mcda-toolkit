---
sidebar_position: 2
---

# Normalization

Normalization is a preprocessing step in the pipeline responsible for bringing all criteria values into a common scale before the decision-making method runs.

## Architecture

Normalization is split into two separate, independent concerns:

- **Transformers** — run first. They adjust values based on criteria type (cost or benefit) before any normalization takes place. They operate independently of the normalization formula itself.
- **Normalizer** — runs after the transformer. It knows only how to apply a specific normalization formula to the already-transformed matrix. It has no knowledge of criteria direction or whether transformation has been applied.

This separation means each part has a single, well-defined responsibility. A normalizer is purely mathematical — it does not inspect criteria types, does not flip values, and does not compensate for direction. That logic belongs entirely to the transformer, and it always happens first.

For a comparison with how normalization worked in versions prior to 5.x, see the [migration guide](../migration/v5#normalization).

## Usage in the pipeline

```csharp
var result =
PipelineBuilder
    .Create()
    //... add data
    .AddProcessingStep(new NormalizationStep<T>()
        .WithMethod(NormalizationMethod.MinMax))
    .Build()
```