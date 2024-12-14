# MCDA Toolkit

[![](https://img.shields.io/nuget/v/McdaToolkit?style=for-the-badge)](https://www.nuget.org/packages/McdaToolkit)
[![License](https://img.shields.io/github/license/SarcasticMoose/mcda-toolkit?style=for-the-badge)](https://github.com/SarcasticMoose/mcda-toolkit/blob/master/LICENSE.txt)

## Introduction
The MCDA Toolkit is a lightweight .NET tool designed for Multi-Criteria Decision Analysis (MCDA).  

## Key feature
- .NET 6 and .NET Standard 2.0
- Easy to use

## Dependencies

- [Math.Net Numerics](https://numerics.mathdotnet.com/)
- [Light Results](https://github.com/jscarle/LightResults)
- [Xunit](https://github.com/xunit/xunit)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)

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
Weights are discribed by double precision numbers ∈<0,1>, and they have to ``sum to 1``.
```csharp
double[] weights = new double[]
{
    0.4,0.25,0.35
};
```

### Prepare types matrix
Types are discribed by two integer numbers ``-1``(cost) and ``1``(profit).
```csharp
int[] types = new int[]
{
    -1,-1,1
};
```
### Run calculation

The final result of the calculation is ``Result<Vector<double>>``.
This object contains information about the success or failure of the operation.
Before obtaining the ``Value`` from the result object, it is necessary to check that no errors have occurred.

```csharp
var matrix = new[,]
    {
        {3, 6, 4, 20, 2, 30000 },
        {4, 4, 6, 15, 2.2, 32000 },
        {6, 5, 9, 18, 3, 32100 },
        {5, 6, 3, 23, 2.8, 28000 },
        {4, 8, 7, 30, 1.5, 29000 },
        {8, 3, 6, 35, 1.9, 27000 },
        {7, 2, 5, 33, 1.7, 28500 },
        {3, 8, 3, 34, 1.6, 30500 },
        {8, 4, 8, 40, 2.5, 33000 },
        {9, 3, 7, 34, 2, 29800 }
    };
double[] weights = [0.1,0.2,0.1,0.2,0.1,0.3];
int[] types = [1,1,1,-1,-1,-1];
        
var data = new DataProviderBuilder()
    .AddWeights(weights)
    .AddDecisionCriteria(types)
    .AddDecisionMatrix(matrix)
    .Build();
        
var vikor = MethodFactory.CreateVikor(new VikorOptions()
{
    NormalizationMethod = NormalizationMethod.Vector,
    VikorParameters = VikorParameters.Create(0.5)
});
        
var vikorResult = vikor.Run(data);
        
var scores = vikorResult
    .Value.Q;
```