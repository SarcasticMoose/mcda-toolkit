using System.Text;
using McdaToolkit.Exporters.Abstraction.FileFormat;
using McdaToolkit.Exporters.Abstraction.FileName;
using McdaToolkit.Exporters.Abstraction.FileName.Generators;

namespace McdaToolkit.Exporters.Abstraction.Path;

public abstract class OutputPathBuilder<TFileFormat>  : 
    IOutputPathBuilder
    where TFileFormat : IFileFormat, new()
{
    private IFileNameGenerator? _fileNameGenerator;
    private IFileFormat _fileFormat = new TFileFormat();
    private string? _directory;

    public IOutputPathBuilder WithDirectory(string directory)
    {
        _directory = directory;
        return this;
    }

    public IOutputPathBuilder WithFileNameGenerator(IFileNameGenerator fileNameGenerator)
    {
        _fileNameGenerator = fileNameGenerator;
        return this;
    }

    public OutputPath Build()
    {
        _fileNameGenerator ??= new DateTimeFileNameGenerator();
        _directory ??= Environment.CurrentDirectory;
        
        var fileName = new StringBuilder();
        fileName.Append(_fileNameGenerator.Generate());
        fileName.Append('.');
        fileName.Append(_fileFormat.Format);

        return new OutputPath(System.IO.Path.Join(_directory, fileName.ToString()));
    }
}