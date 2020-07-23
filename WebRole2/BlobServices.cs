using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1
{
    public class BlobServices
    {
        //private BlobContainerPermissions newBlobContainerPermissions;

        public CloudBlobContainer GetCloudBlobContainer()
        {
            string connString = "DefaultEndpointsProtocol=https;AccountName=r59storageaccount;AccountKey=PsgNpWUH0WP8QI5se5hmzlZaY87k+wDs9lYy5846Z5nCFCw12tXonovKRO0Tc5DRE2cr33FPfvZij45ZVDKquw==;EndpointSuffix=core.windows.net";
            string destContainer = "r59videos";

            // Get a reference to the storage account  
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(destContainer);
            blobContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

            //if (blobContainer.CreateIfNotExists())
            //{
            //    blobContainer.SetPermissions(newBlobContainerPermissions( {PublicAccess = BlobContainerPublicAccessType.Blob });
            //}
            return blobContainer;
        }
    }
}