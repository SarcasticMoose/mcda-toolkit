namespace McdaToolkit.Exporters.Abstraction.Path;

public class OutputPath
{
    private readonly string _fullPath;

    internal OutputPath(string fullPath)
    {
        _fullPath = fullPath;
    }

    public override string ToString()
    {
        return _fullPath;
    }
}