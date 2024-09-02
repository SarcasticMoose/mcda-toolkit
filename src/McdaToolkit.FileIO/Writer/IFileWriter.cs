using System.Threading;
using System.Threading.Tasks;
using LightResults;

namespace McdaToolkit.FileIO.Writer
{
    public interface IFileWriter
    {
        Task<Result> WriteFileAsync(byte[] buffer, string filePath, CancellationToken ct = default);
    }
}