namespace McdaToolkit.Extensions;

internal static class EnumerableExtentions
{
    public static IEnumerable<(T item, int index)> Indexed<T>(this IEnumerable<T> source)
    {
        if (source is null)
        {
            throw new ArgumentNullException();
        }
        
        var i = 0;
        foreach (var item in source)
        {
            yield return (item, i);
            ++i;
        }
    }
}