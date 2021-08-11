using System;
using System.IO;
using System.Text;
using file_ingest_back.Model;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Kms.V1;
using Google.Cloud.Storage.V1;
using Google.Protobuf;
using Newtonsoft.Json;

namespace file_ingest_back.Extensions
{
    public static class ModelExtension
    {
        public static void SaveFile(this IModel model, string path)
        {
            string json = JsonConvert.SerializeObject(model);

            Directory.CreateDirectory(path);
            var file = Path.Combine(path, $"{model.GetJsonFileName()}");

            System.IO.File.WriteAllText(file, json);

            Console.WriteLine($"File Upload ENV: {System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
            var gcp = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine($"Is Prod: {gcp.Equals("Production")}");

            if (gcp.Equals("Production")) 
            {
                Console.Write("starting upload 1 ...");
                try
                {
                    StorageClient storageClient = StorageClient.Create();
                
                    string bucketName = "chimenesjr-ingest-files";

                    UploadFile(bucketName, file, ref storageClient, model.GetJsonFileName());
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.GetExceptionMessages());
                }
            }
        }

        public static void UploadFile(string bucketName, string localPath, ref StorageClient storage, string objectName = null)
        {
            using (var f = File.OpenRead(localPath))
            {
                objectName = objectName ?? Path.GetFileName(localPath);
                try
                {
                    storage.UploadObject(bucketName, objectName, null, f);
                    Console.WriteLine($"Uploaded {objectName}.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.GetExceptionMessages());
                }
                
            }
        }

        public static string GetJsonFileName(this IModel model)
        {
            var initial = model.type.ToString().Substring(0, 1);
            return $"{initial}_{model.Id.ToString()}.json";
        }
    }
}
