using CommandLine;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;

namespace KrokodyliWeb.Backend
{
    public class BackendEntryPoint
    {
        public class CmdArgs
        {
            [Option('d', "data", Required = true, HelpText = "Path to the json file containing data")]
            public string DataFilePath { get; init; } = null!;
            [Option('h', "imaphostname", Required = false, HelpText = "Hostname of the imap server")]
            public string ImapHostName { get; init; } = "imap.gmail.com";
            [Option('p', "imapport", Required = false, HelpText = "Port of the imap server")]
            public int ImapPort { get; init; } = 993;


            [Option('u', "mailusername", Required = true, HelpText = "Username for the mail client")]
            public string MailUserName { get; init; } = null!;
            [Option('w', "mailpassword", Required = true, HelpText = "Password for the mail client")]
            public string MailPassword { get; init; } = null!;
        }

        public static void Main(string[] args)
        {
            var parsed = Parser.Default.ParseArguments<CmdArgs>(args);
            if (parsed.Value == null) return;
            Main(parsed.Value);
        }

#pragma warning disable CS0028
        public static void Main(CmdArgs args)
#pragma warning restore CS0028
        {
            Console.WriteLine($"user: '{args.MailUserName}'\npasswd: '{args.MailPassword}'");
            using var client = new ImapClient();
            client.Connect(args.ImapHostName, args.ImapPort, SecureSocketOptions.SslOnConnect);
            client.Authenticate(args.MailUserName, args.MailPassword);
            client.Inbox.Open(FolderAccess.ReadWrite);
            

            foreach(var uid in client.Inbox.Search(SearchQuery.NotSeen))
            {
                var message = client.Inbox.GetMessage(uid);
                Console.WriteLine($"{uid}--'{message.Subject}':\n{message.TextBody.Substring(0, 10)}...\n\n");
                client.Inbox.MarkRead(uid);
            }
        }


        void s()
        {
            //Google.Apis.Drive.v3.Data.
        }
    }
}