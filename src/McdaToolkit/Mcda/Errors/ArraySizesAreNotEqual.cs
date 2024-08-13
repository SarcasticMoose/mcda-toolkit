using LightResults;

namespace McdaToolkit.Mcda.Errors;

public class ArraySizesAreNotEqual() : Error("Columns length of data matrix should be equal length of weights and types arrays");