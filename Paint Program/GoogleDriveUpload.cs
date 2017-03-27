using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Paint_Program
{
    class GoogleDriveUpload
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "LePaint";

        public static void DriveUpload(string pathToFile)
        {
            // Authenticate with Drive API
            UserCredential credential;
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-lepaint.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Fields = "nextPageToken, files(name)";

            // create a list of files
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            bool exists = false;
            Google.Apis.Drive.v3.Data.File f = null;
            // check if the file exists
            foreach (var file in files)
            {
                if (file.Name == Path.GetFileName(pathToFile))
                {
                    exists = true;
                    f = file;
                    break;
                }
            }

            if(exists)
            {
                // update existing file
                Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                body.Name = System.IO.Path.GetFileName(pathToFile);
                body.MimeType = GetMimeType(pathToFile);

                byte[] byteArray = System.IO.File.ReadAllBytes(pathToFile);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

                try
                {
                    service.Files.Update(body, f.Id, stream, GetMimeType(pathToFile));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error updating existing Drive file: " + e.Message);
                }
            }
            else
            {
                // insert as new file
                Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();
                body.Name = System.IO.Path.GetFileName(pathToFile);
                body.MimeType = GetMimeType(pathToFile);

                byte[] byteArray = System.IO.File.ReadAllBytes(pathToFile);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                try
                {
                    service.Files.Create(body, stream, GetMimeType(pathToFile));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error creating Drive file: " + e.Message);
                }
            }

        }
        
        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}
