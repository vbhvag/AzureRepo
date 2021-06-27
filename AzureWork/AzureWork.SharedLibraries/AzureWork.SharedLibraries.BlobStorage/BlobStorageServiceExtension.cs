using VA.Azure.BlobStorage.Service;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     An extension of Microsoft.Extensions.DependencyInjection.IServiceCollection
    ///     to register dependencies for VA.Azure.BlobStorage.Service
    /// </summary>
    public static class BlobStorageServiceExtension
    {
        /// <summary>
        ///     An extension method to register dependencies for VA.Azure.BlobStorage.Service
        /// </summary>
        /// <param name="services"> Microsoft.Extensions.DependencyInjection.IServiceCollection object reference</param>
        public static void AddBlobStorageService(this IServiceCollection services)
        {
            services.AddSingleton<IBlobStorageService, BlobStorageService>();
        }
    }
}