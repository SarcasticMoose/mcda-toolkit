# McdaToolkit.Exporters.Json

`McdaToolkit.Exporters.Json` provides a JSON-based exporter implementation for **McdaToolkit**.

It builds on top of `McdaToolkit.Exporters.Abstraction` and enables MCDA execution results to be serialized into a structured JSON format suitable for persistence, data exchange, or further processing.

## Purpose

This package is intended to:
- export MCDA results in a **machine-readable JSON format**
- integrate easily with APIs, web applications, and data pipelines
- provide a ready-to-use reference implementation of `IExporter`

## How It Works

### JsonExporter

`JsonExporter` implements the `IExporter` interface and is responsible for transforming `ExecutionDetails` into a JSON representation.

The formatter serializes rankings, criteria, preferences, and execution metadata, and writes the result to the configured output target.

## Example Usage

```csharp
using McdaToolkit.Exporters.Abstraction;
using McdaToolkit.Exporters.Json;

//exectuion context gather from MCDA execution
ExecutionDetails<T> executionDetails = method.Run(data);

var setting = new JsonExporterSettings()
{
    JsonSerializerOptions = new()
    {
        WriteIndented =  true
    },
    Path = new JsonOutputPathBuilder()
        .WithDirectory("test_directory")
        .WithFileNameGenerator(new DateTimeFileNameGenerator())
        .Build()
};

var exporter = new JsonExporterBuilder()
    .WithSettings(new JsonExporterBuilder(_fileWriter)
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
            }))
    .Build();

await exporter.ExportAsync(executionDetails, CancellationToken.None);
```