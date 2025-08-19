using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Models.School.European.Promethee.Gaia;

public record GaiaPlane
{
    public Matrix<double> Projection { get; set; }
    public Matrix<double> Vectors { get; set; }
}