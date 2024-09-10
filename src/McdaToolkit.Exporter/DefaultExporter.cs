using LightResults;
using McdaToolkit.FileIO.Path;
using McdaToolkit.FileIO.Writer;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter
{
    internal class DefaultExporter : IExporter
    {
        private readonly ISerializer _serializer;
        private readonly IFileWriter _fileWriter;
        private readonly IToolkitPath _outputPath;

        public DefaultExporter(ISerializer serializer, IFileWriter fileWriter, IToolkitPath outputPath)
        {
            _serializer = serializer;
            _fileWriter = fileWriter;
            _outputPath = outputPath;
        }

        public Result Export<T>(T data)
            where T : IExportable
        {
            var serializationResult = _serializer.Serialize(data);
            if (serializationResult.IsFailed)
            {
                return Result.Fail(serializationResult.Errors);
            }

            if (_outputPath.Extension != _serializer.Extension)
            {
                _outputPath.ChangeExtension(_serializer.Extension);
            }
            
            var writeResult = _fileWriter.WriteFile(serializationResult.Value, _outputPath);
            return writeResult.IsFailed ? Result.Fail(writeResult.Errors) : Result.Ok();
        }
    }
}