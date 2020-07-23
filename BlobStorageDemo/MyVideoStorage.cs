using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace BlobStorageDemo
{
    public class MyVideoStorage
    {
        private readonly string _connectionString;
        private readonly string _videoContainerName;

        public MyVideoStorage()
        {
            _connectionString = "DefaultEndpointsProtocol=https;AccountName=r59storageaccount;AccountKey=PsgNpWUH0WP8QI5se5hmzlZaY87k+wDs9lYy5846Z5nCFCw12tXonovKRO0Tc5DRE2cr33FPfvZij45ZVDKquw==;EndpointSuffix=core.windows.net";
        }

        public MyVideoStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task UploadVideoAsync(byte[] videoByteArray, string blobName)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(_videoContainerName);
            await cloudBlobContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
            await cloudBlockBlob.UploadFromByteArrayAsync(videoByteArray, 0, videoByteArray.Length);
        }

        public async Task<bool> CheckIfBlobExistsAsync(string blobName)
        {
            return false;
        }

    }
}
