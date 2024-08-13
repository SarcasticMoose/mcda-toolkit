using LightResults;

namespace McdaToolkit.Mcda.Errors;

public class CriteriaNotBetweenMinusOneAndOne() : Error("Criteria decision types should be number ∈Z{-1;1}");