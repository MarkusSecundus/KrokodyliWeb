using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Data
{
    public class WebpageData
    {
        public List<EventInfo>? Events { get; set; }

        public class EventInfo
        {
            public string? RawText { get; set; }

            public List<string>? AttachedImagesUrl { get; set; }

            public DateTimeInterval Time { get; set; }
        }

        public class SummerCampInfo
        {
            public string? RawText { get; set; }

            public int Year { get; set; }

            public string Location { get; set; } = null!;

            public DateTimeInterval Time { get; set; }

            public string Price { get; set; } = null!;

            public string WayOfPayingInfo { get; set; } = null!;
        }
    }
}
