using AzureWork.ConsoleApp.AzureFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureWork.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            AddServices(services, "blobStorage");

            var featureRunner = services.BuildServiceProvider().GetService<IAzureFeatureRunner>();
            featureRunner.Run();
            Console.WriteLine("Feature run completed");
        }

        private static void AddServices(IServiceCollection services, string featureName)
        {
            switch (featureName)
            {
                case "blobStorage":
                    services.AddBlobStorageService();
                    services.AddTransient<IAzureFeatureRunner, BlobStorageRunner>();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}