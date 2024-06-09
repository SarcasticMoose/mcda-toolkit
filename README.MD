# MCDA Toolkit
![NuGet Version](https://img.shields.io/nuget/vpre/McdaToolkit?link=https%3A%2F%2Fwww.nuget.org%2Fpackages%2FMcdaToolkit)

## Introduction
The MCDA Toolkit is a lightweight .NET tool designed for Multi-Criteria Decision Analysis (MCDA).  

## Key feature
- .NET  5+ support

## Datatypes

The ```Matrix<double>``` and ```Vector<double>``` types provided by [Math.Net Numerics](https://numerics.mathdotnet.com/) are used for internal operations.

## Calculation process

### Prepare data matrix

```csharp
double[] data = new double[,]
{
    { 66, 56, 95 },
    { 61, 55, 166 },
    { 65, 49, 113 },
    { 95, 56, 99 },
    { 63, 43, 178 },
    { 74, 59, 140 },
};
```

### Prepare weights matrix 
```csharp
double[] weights = new double[]
{
    0.4,0.25,0.35
};
```

### Prepare types matrix
```csharp
int[] types = new int[]
{
    -1,-1,1
};
```
### Run calculation

```csharp
IMethod topsis = new TopsisMethod();
Vector<double> result = topsis.Calculate(data,weights,types);
```