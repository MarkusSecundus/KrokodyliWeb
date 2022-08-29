using KrokodyliWeb.Utils;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KrokodyliWeb.Backend.BackendEntryPoint;

namespace KrokodyliWeb.Backend
{
    internal class MailProcessor
    {
        public static void ProcessMails(CmdArgs args)
        {
            Console.WriteLine($"user: '{args.MailUserName}'\npasswd: '{args.MailPassword}'");
            using var client = new ImapClient();
            client.Connect(args.ImapHostName, args.ImapPort, SecureSocketOptions.SslOnConnect);
            client.Authenticate(args.MailUserName, args.MailPassword);
            client.Inbox.Open(FolderAccess.ReadWrite);


            foreach (var uid in client.Inbox.Search(SearchQuery.NotSeen))
            {
                var message = client.Inbox.GetMessage(uid);
                Console.WriteLine($"{uid}--'{message.Subject}':\n{message.TextBody.Substring(0, 10)}...\n\n");
                client.Inbox.MarkRead(uid);
            }
        }
    }
}
