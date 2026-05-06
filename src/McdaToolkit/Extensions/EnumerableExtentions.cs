namespace McdaToolkit.Extensions
{
    /// <summary>Extension methods for <see cref="IEnumerable{T}"/>.</summary>
    public static class EnumerableExtentions
    {
        /// <summary>Converts a jagged enumerable to a rectangular 2D array.</summary>
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
}