using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LightResults;

namespace McdaToolkit.FileIO.Writer
{
    public class FileWriter : IFileWriter
    {
        private const int BufferSize = 4096;
    
        public async Task<Result> WriteFileAsync(byte[] buffer, string filePath,CancellationToken ct = default)
        { 
            using (var sourceStream = new FileStream(
                       filePath, 
                       FileMode.Create,
                       FileAccess.Write,
                       FileShare.None,
                       bufferSize: BufferSize, 
                       useAsync: true))
            {
                await sourceStream.WriteAsync(buffer,0,buffer.Length,ct).ConfigureAwait(false);
                return Result.Ok();
            }
        }
    }
}