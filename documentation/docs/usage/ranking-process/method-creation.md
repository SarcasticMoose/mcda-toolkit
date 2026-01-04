---
sidebar_position: 3
---

# Creating method

Each decision-making method requires a specific configuration setup and a distinct set of parameters.
In the toolkit, these configurations are created using dedicated builder classes — one for each method.
These builders provide a fluent interface for setting method-specific and shared parameters, such as the normalization method.

:::warning
Missing values do not throw exceptions, but relying on defaults may lead to unintended behavior or misleading results, especially when the default settings differ from the intended decision model.
:::

Example of **PROMETHEE2** initialization:
```csharp showLineNumbers
var method = Promethee2Builder
    .Create()
    .WithNormalizationMethod(NormalizationMethod.MinMax)
    .WithPreferenceFunction(PreferenceFunction.Unnamed)
    .Build();
```

:::info
After initialization, the method instance remains immutable — its configuration does not change. However, you can supply different input data to the `Run()` method each time it is called, allowing flexible re-use of the same method instance across multiple evaluations.
:::
