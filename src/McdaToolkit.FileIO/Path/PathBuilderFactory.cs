namespace McdaToolkit.FileIO.Path
{
    public static class PathBuilderFactory
    {
        public static IPathBuilder Create()
        {
            return new PathBuilder();
        }
    }
}