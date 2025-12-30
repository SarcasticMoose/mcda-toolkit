namespace McdaToolkit.Exporters.Abstraction.FileWriter.Results.Success;

public record DataWrited(int WritedBytes) : ISuccessFileOperation;