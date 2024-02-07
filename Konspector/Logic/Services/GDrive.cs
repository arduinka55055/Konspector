//ask the end user oauth2 permission to access his google drive and then show his files in console
using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Text.Json;

namespace Konspector.Services
{
    public class GDrive
    {
        public static async Task Run()
        {
            UserCredential credential;
            using (var stream = new FileStream("/Users/denis/Documents/Code/Konspector/.vscode/credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true));
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Konspector",
            });

            var request = service.Files.List();
            request.PageSize = 10;
            request.Fields = "nextPageToken, files(id, name)";

            var result = await request.ExecuteAsync();
            if (result.Files != null && result.Files.Count > 0)
            {
                Console.WriteLine("Files:");
                foreach (var file in result.Files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
        }
    }
}