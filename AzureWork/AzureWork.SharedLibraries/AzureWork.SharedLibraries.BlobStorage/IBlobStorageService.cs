using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureWork.SharedLibraries.BlobStorage
{
    public interface IBlobStorageService
    {
        BlobStorageClient GetContainerClient(string connectionString, string containerName);
    }
}
