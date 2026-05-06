using LightResults;

namespace McdaToolkit.Validation.MatrixValidation.Errors;

/// <summary>Validation error raised when the decision matrix is null.</summary>
public class NullMatrixDataError() : Error("Matrix data cannot be null");