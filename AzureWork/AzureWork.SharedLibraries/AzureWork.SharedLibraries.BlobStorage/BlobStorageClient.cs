using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace VA.AzureWork.SharedLibraries.BlobStorage
{
    public sealed class BlobStorageClient
    {
        private readonly BlobContainerClient _blobClient;
        public BlobStorageClient(BlobContainerClient blobClient)
        {
            _blobClient = blobClient;
        }

        /// <summary>
        /// Summary:
        ///   This method will create a new block blob or overwrite an exiting blob with the given name
        ///   It internally calls the Azure.Storage.Blobs.BlobClient.UploadAsync(Stream content, bool overwrite = false, CancellationToken cancellationToken = default)
        ///   with default values as: 
        ///   overwrite : true => with this setting an existing blob is overwritten
        ///   cancellationToken: default value of cancellation token is send
        /// 
        /// Parameters:
        ///   blobName:
        ///     The name of the blob.
        ///     
        ///   content:
        ///      A System.IO.Stream containing the content to upload.
        /// </summary>  
        public Task UploadBlobAsync(string blobName, Stream content)
        {
            return UploadBlob(blobName, content);
        }

        /// <summary>
        /// Summary:
        ///   This method downloads the blob for a given blob name. 
        ///   It internally calls the  Azure.Storage.Blobs.Specialized.BlobBaseClient.DownloadAsync, which returns an 
        ///   object of Azure.Storage.Blobs.Models.BlobDownloadInfo.Content that contains the blob's data in 
        ///   format System.IO.Stream
        /// 
        /// Parameters:
        ///   blobName:
        ///     The name of the blob.
        ///     
        /// Return:
        ///   A System.IO.Stream containing the content to download.
        ///</summary>
        public Task<Stream> DownloadAsync(string blobName)
        {
            return Download(blobName);
        }

        /// <summary>
        /// Summary:
        ///   This method return a collection of name of blobs in a given container. 
        ///   It internally calls the Azure.Storage.Blobs.BlobContainerClient.GetBlobsAsync, which returns an async sequence of blobs in this container.
        ///     
        /// Return:
        ///   A System.Collections.Generic.List of strings containing the blob names.
        ///</summary>
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