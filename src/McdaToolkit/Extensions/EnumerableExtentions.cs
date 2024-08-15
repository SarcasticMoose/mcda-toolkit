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

    public static T[,] To2DArray<T>(this IEnumerable<IEnumerable<T>> source)
    {
        var sourceToArray = source.ToArray();
        var rows = sourceToArray.Length;
        var cols = sourceToArray[0].Count();
        var result = new T[rows, cols];
        var i = 0;
        
        foreach (var row in sourceToArray)
        {
            var j = 0;
            foreach (var value in row)
            {
                result[i, j] = value;
                j++;
            }
            i++;
        }
        return result;
    }
}