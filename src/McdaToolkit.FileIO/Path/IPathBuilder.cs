using McdaToolkit.Configuration;

namespace McdaToolkit.FileIO.Path
{
    public interface IPathBuilder : IBuilder<Path>
    {
        IPathBuilder WithDirectory(string directoryName);
        IPathBuilder WithFileName(string fileName);
    }
}