using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Backend
{
    internal static class MailUtils
    {
        public static void MarkRead(this IMailFolder self, UniqueId uid, bool read=true)
        {
            if (read)
                self.AddFlags(uid, MessageFlags.Seen, true);
            else
                self.RemoveFlags(uid, MessageFlags.Seen, true);
        }
    }
}
