using Azure.Storage.Blobs;

namespace AzureWork.SharedLibraries.BlobStorage
{
    public class BlobStorageService
    {
        public BlobStorageClient GetContainerClient(string connectionString, string containerName)
        {
            var serviceClient = new BlobServiceClient(connectionString);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();
            return new BlobStorageClient(containerClient);
        }
    }
}