
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VA.AzureWork.SharedLibraries.BlobStorage;

namespace AzureWork.ConsoleApp.AzureFeatures
{
    public class BlobStorageRunner : IAzureFeatureRunner
    {
        private const string connectionString = "UseDevelopmentStorage=true";
        private const string container = "test-container";
        private readonly BlobStorageClient _blobClient;

        public BlobStorageRunner(IBlobStorageService storageService)
        {
            _blobClient = storageService.GetContainerClient(connectionString, container);
        }
        public void Run()
        {
            var blobName = "test-blob-1";
            var content = "Test content to upload on blob storage";

            // UPLOAD BLOB
            var stream = GetStream(content);
            _blobClient.UploadBlobAsync(blobName, stream).GetAwaiter().GetResult();

            //LIST ALL BLOB NAMES
            var blobs = _blobClient.ListAllBlobNames().GetAwaiter().GetResult();

            //DOWNLOAD BLOB
            var dataStream = _blobClient.DownloadAsync(blobName).GetAwaiter().GetResult();
            var data = GetString(dataStream);
        }

        private Stream GetStream(string data)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }

        private string GetString(Stream stream)
        {
            string data = string.Empty;
            stream.Position = 0;
            using (StreamReader reader = new(stream))
            {
                data = reader.ReadToEnd();
            }
            return data;
        }

    }
}
