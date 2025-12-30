using McdaToolkit.Exporters.Abstraction.FileName;

namespace McdaToolkit.Exporters.Abstraction.Path;

public interface IOutputPathBuilder
{
    IOutputPathBuilder WithDirectory(string directory);
    IOutputPathBuilder WithFileNameGenerator(IFileNameGenerator fileNameGenerator);
    OutputPath Build();
}