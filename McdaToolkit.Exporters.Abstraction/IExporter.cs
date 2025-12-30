using McdaToolkit.Data;

namespace McdaToolkit.Exporters.Abstraction;

public interface IExporter
{
    ValueTask ExportAsync(
        ExecutionOutput output,
        CancellationToken ct);
}