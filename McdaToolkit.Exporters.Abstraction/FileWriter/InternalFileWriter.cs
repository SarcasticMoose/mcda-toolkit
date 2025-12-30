using LightResults;
using McdaToolkit.Exporters.Abstraction.FileWriter.Results.Errors;
using McdaToolkit.Exporters.Abstraction.FileWriter.Results.Success;
using McdaToolkit.Exporters.Abstraction.Path;

namespace McdaToolkit.Exporters.Abstraction.FileWriter;

internal sealed class InternalFileWriter : IFileWriter
{
    private readonly int _bufferSize;

    public InternalFileWriter(int bufferSize)
    {
        _bufferSize = bufferSize;
    }
    
    public async ValueTask<Result<ISuccessFileOperation>> WriteToFile(
        Stream stream, 
        OutputPath outputPath,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(outputPath);
        ArgumentNullException.ThrowIfNull(stream);
        
        stream.Position = 0;
        stream.Seek(0, SeekOrigin.Begin);

        try
        {
            await using var fileStream = new FileStream(
                outputPath.ToString(),
                FileMode.Create,
                FileAccess.ReadWrite,
                FileShare.None,
                _bufferSize);

            fileStream.Position = 0;
            fileStream.Seek(0, SeekOrigin.Begin);

            var buffer = new byte[_bufferSize];
            var totalWritedBytes = 0;
            var currentReadBytes = 0;

            while ((currentReadBytes = await stream.ReadAsync(buffer, 0, _bufferSize, ct)) > 0)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, currentReadBytes), ct);
                totalWritedBytes += currentReadBytes;
            }

            if (totalWritedBytes == 0) return new NoneDataWrited();

            await fileStream.FlushAsync(ct);
            await stream.FlushAsync(ct);
            return new DataWrited(totalWritedBytes);
        }
        catch (IOException ex)
        {
            return new IoError(ex);
        }
    }
}