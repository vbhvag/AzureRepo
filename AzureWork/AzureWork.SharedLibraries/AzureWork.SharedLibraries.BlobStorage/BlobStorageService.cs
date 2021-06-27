using Azure.Storage.Blobs;

namespace VA.Azure.BlobStorage.Service
{
    /// <summary>
    ///    This provides an instance of BlobStorageClient that provides all required methods (like: upload, download, list blob names)
    /// </summary>
    public class BlobStorageService: IBlobStorageService
    {
        /// <summary>
        ///   This method will create an instance of BlobStorageClient that provides all required methods (like: upload, download, list blob names)
        /// </summary>
        /// <param name="connectionString">
        ///     A connection string includes the authentication information required for your
        ///     application to access data in an Azure Storage account at runtime. For more information,
        ///     Configure Azure Storage connection strings.
        /// </param>
        /// <param name="containerName">
        ///     The name of the blob container to reference. If container with given name not 
        ///      exists then it will be created
        /// </param>
        /// <returns>
        ///     BlobStorageClient: An object of BlobStorageClient is returned that allows to manipulate Azure Storage
        ///     containers and their blobs.
        /// </returns> 
        public BlobStorageClient GetContainerClient(string connectionString, string containerName)
        {
            var serviceClient = new BlobServiceClient(connectionString);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();
            return new BlobStorageClient(containerClient);
        }
    }
}