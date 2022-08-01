using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Data
{
    public struct DateTimeInterval
    {
        public DateTime Begin { get; }
        public DateTime End { get; }

        public DateTimeInterval(DateTime begin, DateTime end) => (Begin, End) = (begin, end);

        public static implicit operator DateTimeInterval((DateTime, DateTime) i) => new DateTimeInterval(i.Item1, i.Item2);
    }
}
