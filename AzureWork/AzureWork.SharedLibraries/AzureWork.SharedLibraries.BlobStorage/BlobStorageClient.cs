using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace VA.Azure.BlobStorage.Service
{
    /// <summary>
    /// This class is the wrapper over a Azure.Blob.Storage. With this class you can Upload and download the blobs
    /// Also get name of blobs in a given container.
    /// </summary>
    public sealed class BlobStorageClient
    {
        private readonly BlobContainerClient _blobClient;

        /// <summary>
        /// An object of BlobStorageClient can be created by providing an instance of Azure.Storage.Blobs.BlobContainerClient
        /// </summary>
        /// <param name="blobClient">object of Azure.Storage.Blobs.BlobContainerClient</param>
        public BlobStorageClient(BlobContainerClient blobClient)
        {
            _blobClient = blobClient;
        }

        /// <summary>
        ///   This method will create a new block blob or overwrite an exiting blob with the given name
        ///   It internally calls the Azure.Storage.Blobs.BlobClient.UploadAsync(Stream content, bool overwrite = false, CancellationToken cancellationToken = default)
        ///   with default values as: 
        ///   overwrite : true => with this setting an existing blob is overwritten
        ///   cancellationToken: default value of cancellation token is send
        /// </summary>
        /// <param name="blobName">The name of the blob.</param>
        /// <param name="content"> A System.IO.Stream containing the content to upload.</param>
        public Task UploadBlobAsync(string blobName, Stream content)
        {
            return UploadBlob(blobName, content);
        }

        /// <summary>
        ///   This method downloads the blob for a given blob name. 
        ///   It internally calls the  Azure.Storage.Blobs.Specialized.BlobBaseClient.DownloadAsync, which returns an 
        ///   object of Azure.Storage.Blobs.Models.BlobDownloadInfo.Content that contains the blob's data in 
        ///   format System.IO.Stream
        /// </summary>
        /// <param name="blobName">The name of the blob.</param>
        /// <returns>A System.IO.Stream containing the content to download.</returns>
        public Task<Stream> DownloadAsync(string blobName)
        {
            return Download(blobName);
        }

        /// <summary>
        /// Summary:
        ///   This method return a collection of name of blobs in a given container. 
        ///   It internally calls the Azure.Storage.Blobs.BlobContainerClient.GetBlobsAsync, which returns an async sequence of blobs in this container.
        /// </summary>
        /// <returns>A System.Collections.Generic.List of strings containing the blob names.</returns>
        public Task<List<string>> ListAllBlobNames()
        {
            return ListBlobNames();
        }

        private async Task UploadBlob(string blobName, Stream content)
        {
            var blobClient = GetBlobClient(blobName);
            await blobClient.UploadAsync(content, true);
        }

        private async Task<Stream> Download(string blobName)
        {
            var blobClient = GetBlobClient(blobName);
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            var dataStream = new MemoryStream();
            using (var ms = new MemoryStream())
            {
                download.Content.CopyToAsync(ms).GetAwaiter().GetResult();
                ms.Position = 0;
                ms.CopyTo(dataStream);
            }
            return dataStream;
        }

        private async Task<List<string>> ListBlobNames()
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