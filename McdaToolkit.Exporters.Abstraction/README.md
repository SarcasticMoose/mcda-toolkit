# McdaToolkit.Exporters.Abstraction

`McdaToolkit.Exporters.Abstraction` is a foundational package that defines how export functionality is structured within **McdaToolkit**.

It introduces a unified abstraction layer that separates MCDA computation logic from result serialization and output concerns, allowing export mechanisms to evolve independently of the core library.

## Purpose

This package is designed to:
- provide a **single, well-defined export contract**
- maintain **clear separation of concerns** between computation and output
- enable **plug-and-play exporters** for multiple formats

## How It Works

### IExporter

`IExporter` represents the core export contract used by all exporter implementations.
It defines an asynchronous export operation that receives the results of an MCDA execution encapsulated in `ExecutionDetails` and transforms them into a target representation, such as JSON, CSV, or XML.
The operation reports its outcome via `IResult` and supports cooperative cancellation through `CancellationToken`.
By relying on this interface, applications can select or replace export formats without modifying MCDA execution logic, ensuring flexibility and long-term extensibility.