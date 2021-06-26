using VA.AzureWork.SharedLibraries.BlobStorage;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BlobStorageServiceExtension
    {
        public static void AddBlobStorageService(this IServiceCollection services)
        {
            services.AddSingleton<IBlobStorageService, BlobStorageService>();
        }
    }
}