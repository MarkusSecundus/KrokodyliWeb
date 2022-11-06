using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KrokodyliWeb.Data
{
    [XmlRoot(ElementName ="PageLayout")]
    public class NavbarTreeConfig
    {
        [XmlElement("Page", typeof(SimpleNode))]
        [XmlElement("Dropdown", typeof(DropdownNode))]
        public List<Node> Pages { get; set; } = new();


        public abstract class Node {}
        public sealed class Delimiter : Node { }

        public sealed class SimpleNode : Node
        {
            [XmlAttribute("name")]
            public string Name { get; set; } = null!;
            [XmlAttribute("href")]
            public string Href { get; set; } = null!;

            [XmlElement("Param", typeof(QueryParameter))]
            public List<QueryParameter> Parameters { get; set; } = new();

            public class QueryParameter
            {
                [XmlAttribute("name")]
                public string Name { get; set; } = null!;

                [XmlAttribute("value")]
                public string Value { get; set; } = null!;
            }
        }

        public sealed class DropdownNode : Node
        {
            [XmlAttribute("name")]
            public string Name { get; set; } = null!;

            [XmlElement("Page", typeof(SimpleNode))]
            [XmlElement("Delimiter", typeof(Delimiter))]
            public List<Node> Subpages { get; set; } = new();

        }
    }
}
