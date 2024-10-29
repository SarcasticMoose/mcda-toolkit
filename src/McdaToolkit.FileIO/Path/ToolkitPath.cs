namespace McdaToolkit.FileIO.Path
{
    internal class ToolkitPath : IToolkitPath
    {
        public ToolkitPath(string fileName, string directoryPath,string extension)
        {
            FileName = fileName;
            Directory = directoryPath;
            Extension = extension;
        }
        
        public string Directory { get; private set; }
        public string FileName { get; private set; }
        public string Extension { get; private set; }
        public string FullPath => $"{Directory}/{FileName}.{Extension}";
        
        public void ChangeExtension(string extension)
        {
            Extension = extension;
        }
    }
}