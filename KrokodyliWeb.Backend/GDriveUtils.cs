using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Backend
{
    internal static class GDriveUtils
    {
        public static string ImageUrlFromId(string id) => $"https://drive.google.com/uc?id={id}";
    }
}
