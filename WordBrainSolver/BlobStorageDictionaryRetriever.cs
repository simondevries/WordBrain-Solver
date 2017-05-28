using System;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Core
{
    public class BlobStorageDictionaryRetriever : IDictionaryRetriever
    {
        private readonly string _storageConnectionString;

        public BlobStorageDictionaryRetriever(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public string[] RetrieveDictionaryContent()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("dictionary");

            // Retrieve reference to a blob named "myblob.txt"
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("wordsSorted.csv");

            string content;
            using (var memoryStream = new MemoryStream())
            {
                blockBlob2.DownloadToStream(memoryStream);
                content = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}