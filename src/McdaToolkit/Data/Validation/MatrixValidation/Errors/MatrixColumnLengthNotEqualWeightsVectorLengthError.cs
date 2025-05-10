using LightResults;

<<<<<<<< HEAD:src/McdaToolkit/Data/Validation/MatrixValidation/Errors/MatrixColumnLengthNotEqualWeightsVectorLengthError.cs
namespace McdaToolkit.Data.Validation.MatrixValidation.Errors;
========
namespace McdaToolkit.Shared.Validation.MatrixValidation.Errors;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Shared/Validation/MatrixValidation/Errors/MatrixColumnLengthNotEqualWeightsVectorLengthError.cs

public class MatrixColumnLengthNotEqualWeightsVectorLengthError() : Error("Columns length of data matrix should be equal length of weights and types arrays");