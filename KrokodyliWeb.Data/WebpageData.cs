using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Data
{
    public class WebpageData
    {
        public string OrganisationName { get; set; } = null!;
        public List<ScoutYearInfo> Years { get; set; } = new();

        public List<ContactsInfo> Contacts { get; set; } = new();


        public Dictionary<int, string> MembershipNumbers { get; set; } = new();


        public class ScoutYearInfo
        {
            public int Year { get; set; }

            public SummerCampInfo SummerCamp { get; set; } = new();
            public List<EventInfo> Events { get; set; } = new();

            public class EventInfo
            {
                public string? RawText { get; set; }

                public List<string>? AttachedImagesUrl { get; set; }

                public string Name { get; set; } = "";

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

                public string CTH { get; set; } = null!;
            }
        }

        public class ContactsInfo
        {
            public string PersonName { get; set; } = null!;
            public string PhoneNumber { get; set; } = null!;
            public string Email { get; set; } = null!;

        }
    }
}
