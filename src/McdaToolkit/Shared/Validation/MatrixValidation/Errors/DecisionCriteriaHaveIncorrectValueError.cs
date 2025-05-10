using LightResults;

namespace McdaToolkit.Shared.Validation.MatrixValidation.Errors;

public class DecisionCriteriaHaveIncorrectValueError() : Error("Criteria decision types should be number ∈Z{-1;1}");