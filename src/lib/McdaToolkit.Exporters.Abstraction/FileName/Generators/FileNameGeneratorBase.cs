using System.Text;

namespace McdaToolkit.Exporters.Abstraction.FileName.Generators
{
    public abstract class FileNameGeneratorBase : IFileNameGenerator
    {
        protected abstract string GenerateUniqueName();
    
        public string Generate()
        {
            return new StringBuilder()
                .Append(StaticNames.FileFirstSegment)
                .Append('_')
                .Append(GenerateUniqueName())
                .ToString();
        }
    }
}