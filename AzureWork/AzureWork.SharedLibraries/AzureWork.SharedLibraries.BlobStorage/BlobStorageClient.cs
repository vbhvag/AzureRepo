using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AzureWork.SharedLibraries.BlobStorage
{
    public sealed class BlobStorageClient
    {
        private readonly BlobContainerClient _blobClient;
        public BlobStorageClient(BlobContainerClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task UploadBlobAsync(string blobName, Stream fileData)
        {
            var blobClient = GetBlobClient(blobName);
            await blobClient.UploadAsync(fileData, true);
        }

        public async Task<Stream> DownloadAsync(string blobName)
        {
            var blobClient = GetBlobClient(blobName);
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            var dataStream = new MemoryStream();
            using (var ms = new MemoryStream())
            {
                await download.Content.CopyToAsync(ms);
                ms.Position = 0;
                ms.CopyTo(dataStream);
            }
            return dataStream;
        }

        public async Task<List<string>> ListAllBlobNames()
        {
            var blobs = new List<string>();
            await foreach (var item in _blobClient.GetBlobsAsync())
            {
                blobs.Add(item.Name);
            }

            return blobs;
        }

        private BlobClient GetBlobClient(string blobName)
        {
            return _blobClient.GetBlobClient(blobName);
        }
    }
}