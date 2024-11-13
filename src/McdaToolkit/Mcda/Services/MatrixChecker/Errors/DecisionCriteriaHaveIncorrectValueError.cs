using LightResults;

namespace McdaToolkit.Mcda.Services.MatrixChecker.Errors;

public class DecisionCriteriaHaveIncorrectValueError() : Error("Criteria decision types should be number ∈Z{-1;1}");