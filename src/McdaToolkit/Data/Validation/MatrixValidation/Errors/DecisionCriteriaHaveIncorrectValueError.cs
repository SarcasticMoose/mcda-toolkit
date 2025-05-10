using LightResults;

<<<<<<<< HEAD:src/McdaToolkit/Data/Validation/MatrixValidation/Errors/DecisionCriteriaHaveIncorrectValueError.cs
namespace McdaToolkit.Data.Validation.MatrixValidation.Errors;
========
namespace McdaToolkit.Shared.Validation.MatrixValidation.Errors;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Shared/Validation/MatrixValidation/Errors/DecisionCriteriaHaveIncorrectValueError.cs

public class DecisionCriteriaHaveIncorrectValueError() : Error("Criteria decision types should be number ∈Z{-1;1}");