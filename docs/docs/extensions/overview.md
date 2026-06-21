---
sidebar_position: 1
---

# Overview

The toolkit is designed in a modular, package-based architecture. Instead of providing all features in a single monolithic library, functionality is split into separate NuGet packages that you can include only when needed.

This means you install and use only the parts of the system that are relevant to your use case, keeping your project lightweight and avoiding unnecessary dependencies.

## Modular design

Each feature set is delivered as an independent package. These packages extend the core functionality of the toolkit without being part of the base library.

This approach allows you to compose the system based on your requirements rather than relying on a fixed, all-in-one solution.

## Why separate packages?

The core library is intentionally kept minimal. Additional functionality is provided through optional packages that may introduce external dependencies such as:

- file format parsers (CSV, JSON, XML),
- serialization libraries,
- domain-specific processing components.

By separating them, you avoid pulling in dependencies and features you do not use.

## Usage model

You only include the packages that are necessary for your workflow. Each extension integrates seamlessly with the core APIs, without requiring special configuration or additional setup steps.

This keeps the system flexible, lightweight, and easy to maintain while still allowing it to scale in functionality when needed.
