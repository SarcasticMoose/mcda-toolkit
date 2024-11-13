using LightResults;

namespace McdaToolkit.Mcda.Services.MatrixChecker.Errors;

public class MatrixColumnLengthNotEqualWeightsVectorLengthError() : Error("Columns length of data matrix should be equal length of weights and types arrays");