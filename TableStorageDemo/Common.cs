using System;
using Microsoft.WindowsAzure.Storage;


namespace TableStorageDemo
{
    class Common
    {
        public static CloudStorageAccount GetCloudStorageAccount()
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(Microsoft.Azure.CloudConfigurationManager.GetSetting("StorageConnectionString"));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid storage information provided!!");
                throw;
            }
            return storageAccount;
        }
    }
}

