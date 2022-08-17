using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public static class CollectionsUtils
    {
        public static IEnumerable<(T Value, int Index)> WithIndices<T>(this IEnumerable<T> self)
        {
            int index = 0;
            foreach (var item in self)
                yield return (item, index++);
        }

        public static int IndexOfFirst<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            int index = 0;
            foreach(var it in self)
            {
                if (predicate(it)) return index;
                ++index;
            }
            return -1;
        }
    }
}
