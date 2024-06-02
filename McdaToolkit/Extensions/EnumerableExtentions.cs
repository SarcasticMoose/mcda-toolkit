namespace McdaToolkit.Extensions;

internal static class EnumerableExtentions
{
    public static IEnumerable<(T item, int index)> Indexed<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);
        var i = 0;
        foreach (var item in source)
        {
            yield return (item, i);
            ++i;
        }
    }
}