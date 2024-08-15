# MCDA Toolkit

[![](https://img.shields.io/nuget/v/McdaToolkit?style=for-the-badge)](https://www.nuget.org/packages/McdaToolkit)
[![License](https://img.shields.io/github/license/SarcasticMoose/mcda-toolkit?style=for-the-badge)](https://github.com/SarcasticMoose/mcda-toolkit/blob/master/LICENSE.txt)

## Introduction
The MCDA Toolkit is a lightweight .NET tool designed for Multi-Criteria Decision Analysis (MCDA).  

## Key feature
- .NET  6+ support
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
var topsis = new Topsis(new McdaMethodOptions()
{
    NormalizationMethod = NormalizationMethod.MinMax
});
Result<Vector<double>> result = topsis.Calculate(data,weights,types);

if(topsisResult.IsFailed)
{
    foreach (var error in topsisResult.Errors)
    {
        Console.WriteLine(error.Message);
    }
    return;
}
Vector<double> result.Value;
```
More info about ``Result`` type and pattern at: [Light Results](https://github.com/jscarle/LightResults) and [Fluent Results](https://github.com/altmann/FluentResults)