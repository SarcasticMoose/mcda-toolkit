using McdaToolkit.Builder.Abstraction;

namespace McdaToolkit.FileIO.Path
{
    public interface IPathFluentBuilder : IFluentBuilder<IToolkitPath>
    {
        /// <summary>
        /// Set name of the directory
        /// </summary>
        /// <param name="directoryName">If directory not exists, the new one will be created</param>
        /// <returns></returns>
        IPathFluentBuilder WithDirectory(string directoryName);
        /// <summary>
        /// Set the name of the file
        /// </summary>
        /// <param name="fileName">Should be set without extension</param>
        /// <returns></returns>
        IPathFluentBuilder WithFileName(string fileName);
        
        bool OverrideFilesWithSameName { get; set; }
    }
}