using LightResults;

namespace McdaToolkit.Validation.Errors;

internal class UnsupportedNumericTypeError(string typeName)
    : Error($"Type '{typeName}' is not supported. Use double or float");
