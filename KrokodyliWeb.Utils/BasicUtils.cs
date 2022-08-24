using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public static class BasicUtils
    {
        public static T PostAssign<T>(ref T self, T toAssign)
        {
            var ret = self;
            self = toAssign;
            return ret;
        }
    }
}
