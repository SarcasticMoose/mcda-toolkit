using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LightResults;
using McdaToolkit.Exporters.Abstraction.FileWriter.Results.Success;
using McdaToolkit.Exporters.Abstraction.Path;

namespace McdaToolkit.Exporters.Abstraction.FileWriter
{
    public interface IFileWriter
    {
        ValueTask<Result<ISuccessFileOperation>> WriteToFile(
            Stream stream, 
            OutputPath outputPath,
            CancellationToken ct = default);
    }
}