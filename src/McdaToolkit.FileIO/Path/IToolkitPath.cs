namespace McdaToolkit.FileIO.Path
{
    public interface IToolkitPath
    {
        string FullPath { get; }
        string Directory { get; }
        string FileName { get; }
        string Extension { get; }
        void ChangeExtension(string extension);
    }
}