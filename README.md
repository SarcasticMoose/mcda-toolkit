# MCDA Toolkit

[![](https://img.shields.io/nuget/vpre/McdaToolkit?style=for-the-badge&label=Pre-release)](https://www.nuget.org/packages/McdaToolkit)
[![](https://img.shields.io/nuget/v/McdaToolkit?style=for-the-badge&label=Stable)](https://www.nuget.org/packages/McdaToolkit)
[![License](https://img.shields.io/github/license/SarcasticMoose/mcda-toolkit?style=for-the-badge)](https://github.com/SarcasticMoose/mcda-toolkit/blob/master/LICENSE.txt)

**MCDA Toolkit** is a lightweight .NET library designed to support Multi-Criteria Decision Analysis (MCDA) tasks. It helps developers and analysts define and solve decision-making problems, from simple comparisons to more structured evaluation models.

### 🚀 Key Features
**.NET 6** and **.NET Standard 2.0** Support
Compatible with both .NET 6 and .NET Standard 2.0, making it suitable for modern applications as well as legacy or cross-platform projects.

### ⚡ Easy to Use
Designed to be simple and straightforward — no unnecessary setup, no configuration files. You can define and run a decision model with just a few lines of code.

## Documentation

Make sure to read the [docs](https://sarcasticmoose.github.io/mcda-toolkit-docs)

## Dependencies

- [Math.Net Numerics](https://numerics.mathdotnet.com/)
- [Light Results](https://github.com/jscarle/LightResults)

## Quick Example

Here's a simple example to show how to prepare and structure your data for an MCDA calculation using the MCDA Toolkit.

### 1️⃣   Define the Decision Matrix
Each row represents an alternative, and each column corresponds to a criterion.
```csharp
double[,] matrix = new double[,]
{
    { 66, 56, 95 },
    { 61, 55, 166 },
    { 65, 49, 113 },
    { 95, 56, 99 },
    { 63, 43, 178 },
    { 74, 59, 140 },
};
```


### 2️⃣   Prepare Weights
They must be double values between 0 and 1, and must ``sum to 1``
```csharp
double[] weights = new double[]
{
    0.4, 0.25, 0.35
};
```
### 3️⃣   Define Criteria Types

Each criterion is marked as either a cost (-1) or a benefit (1).
```csharp
int[] types = new int[]
{
    -1, -1, 1
};
```

### 4️⃣  Build the Data Object
```csharp
var data = DataBuilder
    .Create()
    .AddWeights(weights)
    .AddDecisionCriteria(types)
    .AddDecisionMatrix(matrix)
    .Build();                    
```

### 5️⃣  Create MCDA method
```csharp
var vikor = VikorBuilder
    .Create()
    .WithNormalizationMethod(NormalizationMethod.Vector)
    .WithVParameter(0.5)
    .Build();
```

### 6️⃣  Run
```csharp
var result = vikor.Run(data);
```

## Outcome

The result of the calculation is returned as an object of type Result<Ranking<T>>, provided by the [LightResults](https://github.com/jscarle/LightResults) library.
It encapsulates both the Ranking<T> and information about whether the operation succeeded or failed.

Example returned success result:

This approach follows the principles of Railway Oriented Programming, 
a functional programming pattern that models the flow of data along two possible tracks — one for success and one for failure. 

More about ``Result`` type can be readed on official [LightResult documentation ](https://jscarle.github.io/LightResults/)

Example returned success result:

```csharp
Ranking<double>()
{
    RankingItems = List<RankingRow<double>>()
    {
        RankingRow<double>
        {
            Alternative = 1,
            Rank = 6,
            Score = 0.417
        },
        RankingRow<double>
        {
            Alternative = 2,
            Rank = 2,
            Score = 0.552
        },
        RankingRow<double>
        {
            Alternative = 3,
            Rank = 4,
            Score = 0.54
        },
        RankingRow<double>
        {
            Alternative = 4,
            Rank = 3,
            Score = 0.54
        },
        RankingRow<double>
        {
            Alternative = 5,
            Rank = 5,
            Score = 0.429
        },
        RankingRow<double>
        {
            Alternative = 6,
            Rank = 1,
            Score = 0.568
        }
    }
};
```