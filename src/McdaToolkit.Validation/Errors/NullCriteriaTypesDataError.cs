using LightResults;

namespace McdaToolkit.Validation.Errors;

/// <summary>Validation error raised when the criteria collection is null.</summary>
public class NullCriteriaTypesDataError() : Error("Criteria decisisons cannot be null");
