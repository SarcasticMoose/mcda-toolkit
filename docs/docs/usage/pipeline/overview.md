---
sidebar_position: 1
---

# Overview

The Pipeline is the central integration point for the entire MCDA process. Every stage of a decision analysis — from input data through method execution to the final ranking — flows through it.

## Quick example

```csharp
var result = PipelineBuilder
    .Create()
    .AddImporter(source)                          // preprocessing — runs first
    .AddNormalization(NormalizationMethod.MinMax)  // preprocessing — runs second
    .Build()
    .Execute(data, method);                       // method + data passed at execution time
```

## Process-oriented design

The entire MCDA workflow is oriented around the Pipeline. Individual building blocks — data, normalizers, methods — exist to be composed into a pipeline, not used as standalone entry points.

## Motivation

The pipeline-oriented design was introduced to address problems that emerged when the evaluation flow was left entirely to the caller:

- **No shared structure.** Each call site assembled the process independently — nothing enforced consistency or captured the intent of the analysis as a whole.
- **Implicit ordering.** The correct sequence of operations was implicit knowledge. Nothing prevented applying steps in the wrong order or skipping one.
- **Scattered coordination logic.** Glue code connecting steps spread across the codebase rather than living in one place.

## Before Pipeline (< 5.x)

Prior to version 5.x, there was no pipeline concept. Each stage was invoked directly — data was prepared, a method instantiated, `Run()` called, and results read back, all as separate loosely connected operations with no shared structure.

The Pipeline was introduced in 5.x as a first-class concept for expressing the entire MCDA process in one place.

If you are upgrading from an earlier version, see the [migration guide](../../migration/v5.md) for a step-by-step breakdown of what changed.
