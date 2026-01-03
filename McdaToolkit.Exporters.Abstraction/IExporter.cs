using LightResults;
using McdaToolkit.Data;

namespace McdaToolkit.Exporters.Abstraction;

public interface IExporter
{
    /// <summary>
    /// Asynchronously exports the provided execution details.
    /// </summary>
    /// <param name="details">
    /// The <see cref="ExecutionDetails"/> containing the results of an MCDA method execution,
    /// including rankings, criteria, and any metadata.
    /// </param>
    /// <param name="ct">A <see cref="CancellationToken"/> to observe while performing the export.</param>
    /// <returns>
    /// A <see cref="ValueTask{IResult}"/> representing the asynchronous operation.
    /// The <see cref="IResult"/> indicates whether the export succeeded or failed.
    /// </returns>
    ValueTask<IResult> ExportAsync(
        ExecutionDetails details,
        CancellationToken ct);
}