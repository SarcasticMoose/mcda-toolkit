namespace McdaToolkit.Exporters.Abstraction.FileName.Generators;

public sealed class DateTimeFileNameGenerator : FileNameGeneratorBase
{
    protected override string GenerateUniqueName()
    {
        return DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
    }
}