﻿using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.NormalizationMethods.Interfaces;

namespace McdaToolkit.NormalizationMethods.Types.Sum;

internal class LogarithmicNormalization : INormalize<double>
{
    /// <summary>
    /// Create normalized vector using logarithmic normalization method 
    /// </summary>
    /// <param name="data">One-dimensional vector of data to normalize</param>
    /// <param name="cost">Describe type of vector, cost or profit</param>
    /// <returns>
    /// Return normalized vector
    /// </returns>
    public Vector<double> Normalize(Vector<double> data, bool cost)
    {
        var product  = data.Aggregate(1.0,(x,y) => x * y);
        var exp = data.PointwiseLog() / Math.Log(product);
        
        if (cost)
        {
            return 1 - exp;
        }

        return exp;
    }
}