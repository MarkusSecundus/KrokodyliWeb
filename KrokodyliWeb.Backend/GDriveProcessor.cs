using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using KrokodyliWeb.Utils;
using MarkusSecundus.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDriveFile = Google.Apis.Drive.v3.Data.File;

namespace KrokodyliWeb.Backend
{
    internal static class GDriveProcessor
    {
        public static async Task Test(string credentialPath)
        {
            GoogleCredential credential;
            // Load client secrets.
            var creds = File.ReadAllText(credentialPath);
            credential = GoogleCredential.FromJson(creds).CreateScoped(DriveService.Scope.Drive);



            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API .NET Quickstart"
            });


            Console.WriteLine(service.BasePath);
            Console.WriteLine(service.BaseUri);

            var rootID = @"1xO4oHGXFeVXjJ3YjbH9IkEQaWaNguwld";  //3 jezy 2021

            var root = await ConstructDirectoryTree(service.Files, rootID);

            if (root == null) Console.WriteLine("null");
            else print(root);

            void print(DriveDirectory root, string indent="")
            {
                Console.WriteLine($"{indent}{root.Descriptor.Name}...");
                foreach (var f in root.Files)
                    Console.WriteLine($"\t{indent}-{f.Name}");
                foreach (var d in root.Subdirectories)
                    print(d, indent + "\t");
            }


        }


        const int MaxPageSize = 1000;
        const string FieldsRequest = "nextPageToken, files(id, name, parents, mimeType)";

        private static async Task<DriveDirectory?> ConstructDirectoryTree(FilesResource service, string rootId)
        {
            var req = service.List();
            req.PageSize = MaxPageSize;
            req.Fields = FieldsRequest;
            req.Q = "mimeType = 'application/vnd.google-apps.folder' and (visibility = 'anyoneCanFind' or visibility = 'anyoneWithLink')";

            var dirs = new DefaultValDict<string, DriveDirectory>(k => new());

            for(; ; )
            {
                var files = await req.ExecuteAsync();

                foreach(var f in files.Files)
                {
                    var curr = dirs[f.Id];
                    curr.Descriptor = f;
                    foreach (var parent in f.Parents)
                        dirs[parent].Subdirectories.Add(curr);
                }

                if ((req.PageToken = files.NextPageToken) == null) break;
            }
            if (!dirs.ContainsKey(rootId))
                return null;

            void ListAllSubdirs(DriveDirectory root, HashSet<string> subdirs)
            {
                if (root.Descriptor == null || subdirs.Contains(root.Descriptor.Id)) return;
                subdirs.Add(root.Descriptor.Id);
                foreach (var s in root.Subdirectories)
                    ListAllSubdirs(s, subdirs);
            }
            var allSubdirIDs = new HashSet<string>();
            ListAllSubdirs(dirs[rootId], allSubdirIDs);

            req = service.List();
            req.PageSize = MaxPageSize;
            req.Fields = FieldsRequest;
            req.Q = $"(mimeType != 'application/vnd.google-apps.folder') and (parents in '{allSubdirIDs.MakeString("' or parents in '")}')";
            
            for(; ; )
            {
                var files = await req.ExecuteAsync();

                foreach(var f in files.Files)
                {
                    foreach (var parent in f.Parents)
                        dirs[parent].Files.Add(f);
                }

                if ((req.PageToken = files.NextPageToken) == null) break;
            }

            return dirs[rootId];
        }

        class DriveDirectory
        {
            public GDriveFile Descriptor { get; set; } = null!;

            public List<GDriveFile> Files { get; } = new();

            public List<DriveDirectory> Subdirectories { get; } = new();
        }

    }
}
