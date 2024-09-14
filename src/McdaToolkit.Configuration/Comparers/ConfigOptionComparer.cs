using System;
using System.Collections.Generic;

namespace McdaToolkit.Configuration.Comparers
{
    public class ConfigOptionKeyComparer : IEqualityComparer<IConfigOption>
    {
        public bool Equals(IConfigOption? x, IConfigOption? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Key == y.Key;
        }

        public int GetHashCode(IConfigOption obj)
        {
            return HashCode.Combine(obj.Key, obj.Value);
        }
    }
}