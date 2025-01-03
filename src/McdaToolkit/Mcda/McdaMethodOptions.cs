﻿using McdaToolkit.Normalization.Enums;

namespace McdaToolkit.Mcda;

/// <summary>
/// Configuration for mcda methods
/// </summary>
public interface IMcdaMethodOptions
{
    /// <summary>
    /// Normalization method
    /// </summary>
    public NormalizationMethod NormalizationMethod { get; set; }
}