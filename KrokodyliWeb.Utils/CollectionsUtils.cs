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

        public static IEnumerable<(T Value, int Index)> IterateSliceWithIndices<T>(this IReadOnlyList<T> self, int? begin, int? end=null)
        {
            int e = end??self.Count;

            for (int t = begin??0; t < e; ++t)
                yield return (self[t], t);
        }
        public static IEnumerable<(T Value, int Index)> IterateSliceWithIndices<T>(this ListView<T> self)
            => self.List.IterateSliceWithIndices(self.Begin, self.End);



        public static ListView<T> AddSegment<T>(this List<T> self, IEnumerable<T> toAdd)
        {
            var begin = self.Count;
            self.AddRange(toAdd);
            return new ListView<T>(self, begin, self.Count);
        }
        public static ListView<T> AddSegment<T>(this List<T> self, params T[] toAdd)
            => self.AddSegment((IEnumerable<T>)toAdd);

        public static ListElementView<T> AddElement<T>(this List<T> self, T toAdd)
        {
            self.Add(toAdd);
            return new ListElementView<T>(self, self.Count-1);
        }

        public static ListView<T> AsView<T>(this IReadOnlyList<T> self)
            => new ListView<T>(self);
    }
}
