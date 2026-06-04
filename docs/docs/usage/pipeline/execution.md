---
sidebar_position: 4
---

# Execution

Once built, the pipeline is executed by calling `Execute()` and passing both the problem data and the method. Neither is baked into the pipeline at build time — they're supplied at the point of execution.

## Running the pipeline

```csharp
var result = pipeline.Execute(data, method);
```

`Execute()` triggers the full sequence: all preprocessing steps run in order on the provided data, and the method receives the result. The pipeline returns a `Ranking<T>` once execution completes.

## One-shot evaluation

```csharp
var result = PipelineBuilder
    .Create()
    .AddNormalization(NormalizationMethod.MinMax)
    .Build()
    .Execute(data, method);
```

## Reusing a pipeline

Because the method and data are passed to `Execute()` rather than baked into the pipeline, the same pipeline instance can be reused across different inputs:

```csharp
var resultA = pipeline.Execute(dataA, method);
var resultB = pipeline.Execute(dataB, method);
```

This is useful when comparing the same preprocessing configuration across multiple datasets or methods.

## Error handling

Data validation failures surface during `Execute()`. No state is retained between calls — a failed execution has no effect on the next one.
