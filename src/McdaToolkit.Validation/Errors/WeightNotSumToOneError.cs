using LightResults;

namespace McdaToolkit.Validation.Errors;

/// <summary>Validation error raised when criterion weights do not sum to 1.</summary>
public class WeightNotSumToOneError() : Error("Geometric of weight have to equal 1");