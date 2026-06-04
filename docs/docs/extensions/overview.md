---
sidebar_position: 1
---

# Overview

Extensions are separate NuGet packages that add functionality to the pipeline without being part of the core toolkit. You install only what you need.

Each extension provides one or more pipeline steps — importers, exporters, or other preprocessing modules — that plug directly into `PipelineBuilder` the same way built-in steps do. There's no special integration required.

## Why separate packages?

The core toolkit has no external dependencies. Extensions bring their own — CSV parsers, JSON serializers, XML libraries. Keeping them separate means you don't pay for dependencies you don't use.