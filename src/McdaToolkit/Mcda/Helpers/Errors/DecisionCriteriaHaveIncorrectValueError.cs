using LightResults;

namespace McdaToolkit.Mcda.Helpers.Errors;

public class DecisionCriteriaHaveIncorrectValueError() : Error("Criteria decision types should be number ∈Z{-1;1}");