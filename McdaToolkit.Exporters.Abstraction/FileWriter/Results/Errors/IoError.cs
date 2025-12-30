using LightResults;

namespace McdaToolkit.Exporters.Abstraction.FileWriter.Results.Errors;

public class IoError(Exception exception) : Error(exception.Message);