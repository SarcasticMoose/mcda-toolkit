using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions.Abstraction;

public abstract class PreferenceFunctionBase : IPreferenceFunction
{
    public Matrix<double> Execute(Matrix<double> input)
    {
        var output = Matrix<double>.Build.Dense(input.RowCount, input.ColumnCount);
        foreach (var (i,row) in input.EnumerateRowsIndexed())
        {
            output.InsertRow(i, ExecuteRow(row));
        }
        return output;
    }

    private Vector<double> ExecuteRow(Vector<double> input)
    {
        var output = Vector<double>.Build.Dense(input.Count);
        foreach (var d in input)
        {
            output.Add(ExecuteOne(d));
        }
        return output;
    }
    
    public abstract double ExecuteOne(double d);
}