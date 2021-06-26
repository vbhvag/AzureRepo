using AzureWork.ConsoleApp.AzureFeatures;
using System;

namespace AzureWork.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var featureRunner = AzureFeatureRunnerFactory("blobStorage");
            featureRunner.Run();

            Console.WriteLine("Feature run completed");
        }

        private static IAzureFeatureRunner AzureFeatureRunnerFactory(string featureName)
        {
            switch (featureName)
            {
                case "blobStorage":
                    return new BlobStorageRunner();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}