using LightResults;
using McdaToolkit.FileIO.Path;

namespace McdaToolkit.FileIO.Writer
{
    public interface IFileWriter
    {
        Result WriteFile(string text, IToolkitPath filePath);
    }
}