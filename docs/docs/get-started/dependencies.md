---
sidebar_position: 2
---

# Dependencies
Project utilizes a selection of libraries that help speed up development and improve the quality of the code. 
These libraries provide functionality that allows for faster implementation, reduced boilerplate code, and optimized performance for numerically intensive tasks.

## LightResults

[LightResults](https://jscarle.github.io/LightResults/) is a simple, lightweight library designed to improve error handling by using a Railway Oriented
Programming model that came from functional programming world. Rather than using exceptions to manage errors, 
LightResults introduces two primary states: Success and Failure.

## Math.Net Numerics

[Math.NET Numerics](https://numerics.mathdotnet.com/) supports optional integration with native linear algebra backends for dramatically better performance on large numerical computations:
* Linear algebra (vectors, matrices)
* Statistics and probability distributions

:::info

**Math.NET Numerics** provides native support for high-performance math operations library called **Intel MKL**.
Usage of this provider can be read on [docs](https://numerics.mathdotnet.com/MKL)
:::