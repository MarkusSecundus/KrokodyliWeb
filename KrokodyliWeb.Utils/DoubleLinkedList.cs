using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public class DoubleLinkedList<TValue> : IIntrusiveLinkedList<DoubleLinkedList<TValue>>
    {
        public TValue Value { get; }


        public DoubleLinkedList(TValue value)
        {
            Value = value;
            Last = Next = this;
        }

        public DoubleLinkedList<TValue> Last { get; set; }
        public DoubleLinkedList<TValue> Next { get; set; }

        public int Count => this.Iterate().Count();

        public static implicit operator DoubleLinkedList<TValue>(TValue value) => new DoubleLinkedList<TValue>(value);

        public override string ToString() => $"#{this.Iterate().Count()}#{Value.ToString()}";

        public static DoubleLinkedList<TValue> FromArray(IEnumerable<TValue> values)
        {
            DoubleLinkedList<TValue> ret = null;

            foreach (var v in values)
                ret = ret.AppendList(v);

            return ret;
        }

        public static DoubleLinkedList<TValue> FromArray(params TValue[] values) => FromArray((IEnumerable<TValue>)values);
    }
}
