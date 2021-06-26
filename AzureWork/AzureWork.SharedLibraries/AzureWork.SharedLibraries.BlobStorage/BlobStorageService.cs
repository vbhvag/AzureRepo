using Azure.Storage.Blobs;

namespace VA.AzureWork.SharedLibraries.BlobStorage
{
    public class BlobStorageService: IBlobStorageService
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