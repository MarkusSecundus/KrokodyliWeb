using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public class EmbeddedResourceReader
    {
        private readonly MarkusSecundus.Util.DefaultValDict<(Assembly Assembly, string ResourceName), string> cache = new(t=> t.Assembly.ReadEmbeddedResourceText(t.ResourceName));

        public string GetString(Assembly assembly, string resourceName) => cache[(assembly, resourceName)];
    }
}
