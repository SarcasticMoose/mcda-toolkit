using LightResults;

namespace McdaToolkit.McdaMethods.Errors;

public class CriteriaNotBetweenMinusOneAndOne() : Error("Criteria decision types should be number ∈Z{-1;1}");