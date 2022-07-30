using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Backend
{
    internal class WebpageData
    {
        public List<EventInfo>? Events { get; set; }

        public class EventInfo
        {
            public string? RawText { get; set; }

            public List<string>? AttachedImagesUrl { get; set; }
        }
    }
}
