using System.Configuration;
using System.IO;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types

namespace WordBrainSolver
{
    public class DictionaryRetriever
    {
        public string GetDictionary()
        {
            //todo Sdv this should not need to download every time


            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings.Get("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("dictionary");

            // Retrieve reference to a blob named "myblob.txt"
            CloudBlockBlob blockBlob2 = container.GetBlockBlobReference("wordsSorted.csv");

            string text;
            using (var memoryStream = new MemoryStream())
            {
                blockBlob2.DownloadToStream(memoryStream);
                text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return text;
        }
    }
}