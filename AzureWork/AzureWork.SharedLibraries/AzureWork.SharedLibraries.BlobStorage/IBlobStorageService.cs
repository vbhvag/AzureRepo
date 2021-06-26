namespace VA.AzureWork.SharedLibraries.BlobStorage
{
    
    public interface IBlobStorageService
    {
        /// Summary:
        ///   This method will create an instance of BlobStorageClient that all required methods (like: upload, download, list blob names)
        /// 
        /// Parameters:
        ///   connectionString:
        ///     A connection string includes the authentication information required for your
        ///     application to access data in an Azure Storage account at runtime. For more information,
        ///     Configure Azure Storage connection strings.
        ///     
        ///   containerName:
        ///      The name of the blob container to reference. If container with given name not 
        ///      exists then it will be created
        ///     
        /// Returns:
        ///    BlobStorageClient:
        ///      An object of BlobStorageClient is returned that allows to manipulate Azure Storage
        ///      containers and their blobs.
        ///      
        BlobStorageClient GetContainerClient(string connectionString, string containerName);
    }
}