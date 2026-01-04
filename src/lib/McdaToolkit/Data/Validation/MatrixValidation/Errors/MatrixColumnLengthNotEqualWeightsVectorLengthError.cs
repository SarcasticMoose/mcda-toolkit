using LightResults;

namespace McdaToolkit.Data.Validation.MatrixValidation.Errors;

public class MatrixColumnLengthNotEqualWeightsVectorLengthError() : Error("Columns length of data matrix should be equal length of weights and types arrays");