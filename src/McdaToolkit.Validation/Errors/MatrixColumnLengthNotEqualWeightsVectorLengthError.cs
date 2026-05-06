using LightResults;

namespace McdaToolkit.Validation.MatrixValidation.Errors;

/// <summary>Validation error raised when the number of matrix columns does not match the number of criteria.</summary>
public class MatrixColumnLengthNotEqualWeightsVectorLengthError() : Error("Columns length of data matrix should be equal length of weights and types arrays");