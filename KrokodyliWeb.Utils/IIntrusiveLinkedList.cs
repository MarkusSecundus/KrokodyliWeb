using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public interface IIntrusiveLinkedList<TSelf> where TSelf: class, IIntrusiveLinkedList<TSelf>
    {
        public TSelf Last { get; internal protected set; }
        public TSelf Next { get; internal protected set; }
    }


    public static class IntrusiveLinkedListExtensions
    {
        public static bool IsSingle<T>(this T self) where T : class, IIntrusiveLinkedList<T>
        {
            return ((self == self.Next).i() + (self == self.Last).i()) switch
            {
                2 => true,
                0 => false,
                _ => throw new InvalidDataException($"Malformed linked list {self}")
            };
        }

        public static T RemoveFirst<T>(this T self) where T: class, IIntrusiveLinkedList<T>
        {
            if (self == null || self.IsSingle()) return null;
            var ret = self.Next;
            self.Last.Next = self.Next;
            self.Next.Last = self.Last;
            self.Next = self.Last = self;
            return ret;
        }

        public static T RemoveEnd<T>(this T self) where T : class, IIntrusiveLinkedList<T>
        {
            if (self==null || self.IsSingle()) return null;
            self.Last.RemoveFirst();
            return self;
        }

        public static T AppendList<T>(this T self, T toAppend) where T : class, IIntrusiveLinkedList<T>
        {
            if (self == null) return toAppend;
            if (toAppend == null) return self;

            var selfLast = self.Last;
            var toAppendLast = toAppend.Last;
            selfLast.Next = toAppend;
            toAppend.Last = selfLast;
            self.Last = toAppendLast;
            toAppendLast.Next = self;
            return self;
        }

        public static IEnumerable<T> Iterate<T>(this T self) where T : class, IIntrusiveLinkedList<T>
        {
            if (self == null) yield break;
            T it = self;
            do
            {
                yield return it;
                it = it.Next;
            } while (it != self);
        }
        public static IEnumerable<T> IterateBackwards<T>(this T self) where T : class, IIntrusiveLinkedList<T>
        {
            if (self == null) yield break;
            T it = self;
            do
            {
                yield return it;
                it = it.Last;
            } while (it != self);
        }
    }
}
