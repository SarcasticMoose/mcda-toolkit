using McdaToolkit.Data;
using McdaToolkit.Exporters.Abstraction.FileName.Generators;
using McdaToolkit.Exporters.Abstraction.FileWriter;
using NSubstitute;

namespace McdaToolkit.Exporters.Json.UnitTests;

public class JsonExporterTests
{
    private IFileWriter _fileWriter;

    [SetUp]
    public void Setup()
    {
        _fileWriter = Substitute.For<IFileWriter>();
    }

    [Test]
    public async Task Test1()
    {
        var setting = new JsonExporterSettings()
        {
            JsonSerializerOptions = new()
            {
                WriteIndented =  true
            },
            Path = new InternalJsonOutputPathBuilder()
                .WithDirectory("test_directory")
                .WithFileNameGenerator(new DateTimeFileNameGenerator())
                .Build()
        };
        var exporter = new JsonExporterBuilder(_fileWriter)
            .WithSettings(setting)
            .Build();
        
        await exporter.ExportAsync(new ExecutionDetails()
        {
        }, CancellationToken.None);
    }
}