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
        var exporter = new JsonExporterBuilder(_fileWriter)
            .WithSettings(new JsonExporterSettings()
            {
                JsonSerializerOptions = new()
                {
                    WriteIndented = true
                },
                Path = new JsonOutputPathBuilder()
                    .WithDirectory("test_directory")
                    .WithFileNameGenerator(new DateTimeFileNameGenerator())
                    .Build()
            })
            .Build();
        
        await exporter.ExportAsync(new ExecutionDetails()
        {
        }, CancellationToken.None);
    }
}