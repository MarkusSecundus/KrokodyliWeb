using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public static class CollectionsUtils
    {

        public static int i(this bool self) => self ? 1 : 0;
        public static IEnumerable<(T Value, int Index)> WithIndices<T>(this IEnumerable<T> self)
        {
            int index = 0;
            foreach (var item in self)
                yield return (item, index++);
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> self)
        {
            foreach (var it in self)
                foreach (var y in it) yield return y;
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

        public static bool CountIsLessThan<T>(this IEnumerable<T> self, int count)
        {
            foreach(var _ in self)
            {
                if (--count < 0) return false;
            }
            return true;
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


        public static string ReadEmbeddedResourceText(this Assembly self, string resourceName)
        {
            resourceName = self.GetName().Name + "." + resourceName;
            using var stream = self.GetManifestResourceStream(resourceName);
            if (stream == null) return null;
            using var rdr = new StreamReader(stream);
            return rdr.ReadToEnd();
        }


        /*public static string MakeString<T>(this IEnumerable<T> self, string separator=", ")
        {
            if (self == null) return "";
            using var it = self.GetEnumerator();
            if (!it.MoveNext()) return "";
            var bld = new StringBuilder().Append(it.Current);

            while (it.MoveNext()) bld.Append(separator).Append(it.Current);

            return bld.ToString();
        }*/
    }
}
