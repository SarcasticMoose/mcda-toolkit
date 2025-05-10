using LightResults;

<<<<<<<< HEAD:src/McdaToolkit/Data/Validation/MatrixValidation/Errors/WeightNotSumToOneError.cs
namespace McdaToolkit.Data.Validation.MatrixValidation.Errors;
========
namespace McdaToolkit.Shared.Validation.MatrixValidation.Errors;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Shared/Validation/MatrixValidation/Errors/WeightNotSumToOneError.cs

public class WeightNotSumToOneError() : Error("Geometric of weight have to equal 1");