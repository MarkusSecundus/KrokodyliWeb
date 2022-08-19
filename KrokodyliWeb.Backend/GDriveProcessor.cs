using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Backend
{
    internal static class GDriveProcessor
    {
        public static void Test(string credentialPath)
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


                
            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 1000;
            listRequest.Fields = "nextPageToken, files(id, name)";

            while (true)
            {
                // List files.
                var files = listRequest.Execute();

                Console.WriteLine("Files:");
                Console.WriteLine($"{files.Files.Count} files...");
                foreach (var file in files.Files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }

                if (files.NextPageToken == null) break;

                listRequest.PageToken = files.NextPageToken;
            }
        }
    }
}
