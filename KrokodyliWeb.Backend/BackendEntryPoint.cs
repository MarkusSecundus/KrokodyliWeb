using CommandLine;
using KrokodyliWeb.Data;
using KrokodyliWeb.Utils;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using System.Text.Json;

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
            new BackendEntryPoint(parsed.Value).Run();
        }



        private readonly CmdArgs args;
        private readonly WebpageData data;
        public BackendEntryPoint(CmdArgs args) {
            this.args = args;
            this.data = GetWebpageData(args);
        }


        public void Run()
        {
            data.Contacts.Add(new()
            {
                Email = "krokodyli@skaut.cz",
                PhoneNumber = "605485388",
                PersonName = "Martina Barvířová"
            });


            SaveWebpageData();
        }



        private static WebpageData GetWebpageData(CmdArgs args)
        {
            try
            {
                using var file = File.OpenRead(args.DataFilePath);
                return JsonSerializer.Deserialize<WebpageData>(file) ?? new();
            }catch(Exception e) when (e is IOException || e is JsonException)
            {
                return new();
            }
        }

        private void SaveWebpageData()
        {
            using var file = File.OpenWrite(args.DataFilePath);
            JsonSerializer.Serialize(file, data);
        }

        private static void ProcessMails(CmdArgs args)
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