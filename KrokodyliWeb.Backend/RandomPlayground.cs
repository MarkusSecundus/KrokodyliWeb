using KrokodyliWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KrokodyliWeb.Backend
{
    internal class RandomPlayground
    {
        public void TestNavbarTreeConfig()
        {
            var c = new NavbarTreeConfig
            {
                Pages = new List<NavbarTreeConfig.Node>()
                {
                    new NavbarTreeConfig.SimpleNode { Name = "Domů", Href = "index" },
                    new NavbarTreeConfig.DropdownNode
                    { Name = "O Nás",
                      Subpages = new List<NavbarTreeConfig.Node>
                        {
                            new NavbarTreeConfig.SimpleNode { Name = "O Nás", Href = "pages/about-us" },
                            new NavbarTreeConfig.Delimiter(),
                            new NavbarTreeConfig.SimpleNode { Name = "Naše parta", Href = "pages/about-us/our-team" },
                            new NavbarTreeConfig.SimpleNode { Name = "Naše činnost", Href = "pages/about-us/our-work" },
                            new NavbarTreeConfig.SimpleNode { Name = "Kde nás najdete", Href = "pages/about-us/where-to-find-us" },
                        }
                    },
                    new NavbarTreeConfig.SimpleNode{Name="Akce", Href="events"},
                    new NavbarTreeConfig.SimpleNode{Name="Tábor", Href="pages/summer-camp"},
                    new NavbarTreeConfig.DropdownNode
                    { Name = "Pro členy",
                      Subpages = new List<NavbarTreeConfig.Node>
                        {
                            new NavbarTreeConfig.SimpleNode{Name="Pro členy", Href="pages/for-members"},
                            new NavbarTreeConfig.Delimiter(),
                            new NavbarTreeConfig.SimpleNode{Name="Kronika", Href="pages/for-members/chronicles"},
                            new NavbarTreeConfig.SimpleNode{Name="Bodování", Href="pages/for-members/scores"},
                            new NavbarTreeConfig.SimpleNode{Name="Stezka", Href="pages/for-members/stezka"},
                            new NavbarTreeConfig.SimpleNode{Name="Pomůcky na schůzky a akce", Href="pages/for-members/accessories-list"},
                            new NavbarTreeConfig.SimpleNode{Name="Kroj", Href="pages/for-members/uniforms"},
                            new NavbarTreeConfig.SimpleNode{Name="Čísla členů", Href="for-members/numbering"},
                        }
                    },
                    new NavbarTreeConfig.SimpleNode{Name="Fotogalerie", Href="fotogalery"},
                    new NavbarTreeConfig.SimpleNode{Name="Podpořte nás!", Href="pages/how-to-support-us"},
                    new NavbarTreeConfig.SimpleNode{Name="Odkazy", Href="pages/references"},
                    new NavbarTreeConfig.SimpleNode{Name="Kontakty", Href="pages/contacts"},
                }
            };

            StringWriter str = new(), str2 = new();

            var ser = new XmlSerializer(typeof(NavbarTreeConfig));
            var wrt = XmlWriter.Create(str, new() { Indent = true, OmitXmlDeclaration = true });
            var nmsp = new XmlSerializerNamespaces();
            nmsp.Add("", "");
            var wrt2 = XmlWriter.Create(str2, new() { Indent = true, Encoding = Encoding.UTF8 });
            ser.Serialize(wrt, c, nmsp);


            c = (NavbarTreeConfig?)ser.Deserialize(new StringReader(str.ToString()));
            ser.Serialize(wrt2, c);

            Console.WriteLine(str.ToString());
            Console.WriteLine("\n-------------------------------------------------------------------------------------------\n\n");
            Console.WriteLine(str2.ToString());
        }
    }
}
