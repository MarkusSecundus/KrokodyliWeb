using CommandLine;
using KrokodyliWeb.Data;
using KrokodyliWeb.Utils;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using System.Text.Encodings.Web;
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

            [Option('c', "gcloudCredentialPath", Required = true, HelpText = "Path to credential json for Google Cloud API")]
            public string GCloudCredentialPath { get; init; } = null!;
        }

        public static async Task Main(string[] args)
        {
            var parsed = Parser.Default.ParseArguments<CmdArgs>(args);
            if (parsed.Value == null) return;
            await new BackendEntryPoint(parsed.Value).Run();
        }



        private readonly CmdArgs args;
        private readonly WebpageData data;
        public BackendEntryPoint(CmdArgs args) {
            this.args = args;
            this.data = GetWebpageData(args);
        }


        public async Task Run()
        {
            await GDriveProcessor.Test(args.GCloudCredentialPath);


            SaveWebpageData();
        }



        private static readonly JsonSerializerOptions DeserializeOptions = new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

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
            JsonSerializer.Serialize(file, data, options: DeserializeOptions);
        }

    }
}