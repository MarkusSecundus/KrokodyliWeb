using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public struct ListView<T>
    {
        public IReadOnlyList<T> List { get; }

        public int Begin { get; }
        public int End { get; }

        public ListView(IReadOnlyList<T> list, int? begin=null, int? end=null)
            => (List, Begin, End) = (list, begin ?? 0, end ?? list.Count);


        public static readonly ListView<T> Empty = new ListView<T>(Array.Empty<T>());

        public static implicit operator ListView<T>(List<T> list) => new ListView<T>(list);
        public static implicit operator ListView<T>(T[] list) => new ListView<T>(list);
        public static implicit operator ListView<T>(ListElementView<T> ev) => new ListView<T>(ev.List, ev.Index, ev.Index+1);
    }

    public struct ListElementView<T>
    {
        public IReadOnlyList<T> List { get; }

        public int Index { get; }

        public T Value => List[Index];

        public ListElementView(IReadOnlyList<T> list, int index)
            => (List, Index) = (list, index);

        public static implicit operator ListElementView<T>(T elem) => new ListElementView<T>(new[] {elem}, 0);
        //public static implicit operator T(ListElementView<T> v) => v.Value;
    }
}
