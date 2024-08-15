using LightResults;

namespace McdaToolkit.Mcda.Helpers.Errors;

public class MatrixColumnLengthNotEqualWeightsVectorLengthError() : Error("Columns length of data matrix should be equal length of weights and types arrays");