using System;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LightResults;
using McdaToolkit.FileIO.Path;

namespace McdaToolkit.FileIO.Writer
{
    public class FileWriter : IFileWriter
    {
        private readonly IFileSystem _fileSystem;
        private const int BufferSize = 4096;

        public FileWriter(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        
        public Result WriteFile(string text, IToolkitPath filePath)
        {
            _fileSystem.Directory.CreateDirectory(filePath.Directory);
            
            using (var sourceStream = _fileSystem.FileStream.New(
                       filePath.FullPath, 
                       FileMode.Create,
                       FileAccess.Write,
                       FileShare.None,
                       bufferSize: BufferSize, 
                       useAsync: true))
            {
                var buffer = Encoding.Unicode.GetBytes(text);
                sourceStream.Write(buffer);
                return Result.Ok();
            }
        }
    }
}