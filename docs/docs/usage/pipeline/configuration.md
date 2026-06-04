---
sidebar_position: 3
---

# Configuration

The Pipeline is built using a dedicated builder that follows the same fluent interface pattern used throughout the toolkit.

## Building a Pipeline

The builder collects preprocessing steps. The method and data are not part of the pipeline configuration — they're passed at execution time.

```csharp
var pipeline = PipelineBuilder
    .Create()
    .AddNormalization(NormalizationMethod.MinMax)
    .Build();
```

This means the same pipeline instance works with any data and any method — you decide at `Execute()` time, not at build time.

:::info
The same pipeline instance can be reused across multiple calls without rebuilding. Preprocessing configuration is fixed at `Build()` time; data and method are always supplied fresh at `Execute()`.
:::

## Reusing a Pipeline

```csharp
var resultA = pipeline.Execute(dataA, methodA);
var resultB = pipeline.Execute(dataB, methodB);
```

Useful when comparing multiple scenarios or methods through the same preprocessing configuration.

:::warning
Changing preprocessing configuration requires rebuilding the pipeline. The builder does not expose mutation — all steps are fixed at `Build()` time.
:::
