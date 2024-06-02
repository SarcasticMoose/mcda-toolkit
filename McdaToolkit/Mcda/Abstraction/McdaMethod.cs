using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Enums;
using McdaToolkit.Normalization;
using McdaToolkit.Normalization.Abstraction;

namespace McdaToolkit.Mcda.Abstraction;

public abstract class McdaMethod : IMethod
{
    protected IDataNormalization DataNormalization = new DataNormalization(NormalizationMethod.MinMax);

    public abstract MathNet.Numerics.LinearAlgebra.Vector<double> Calculate(
        double[,] matrix,
        double[] weights,
        int[] criteriaDirections);
}