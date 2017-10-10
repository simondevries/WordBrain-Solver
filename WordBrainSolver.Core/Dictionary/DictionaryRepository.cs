using System;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Dictionary
{
    /// <summary>
    /// Responsible for retrieving the dictionary
    /// </summary>
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly string _storageConnectionString;
        private readonly IWordDictionariesCacheService _wordDictionariesCacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryRepository"/> class.
        /// </summary>
        public DictionaryRepository(string storageConnectionString, IWordDictionariesCacheService wordDictionariesCacheService)
        {
            _storageConnectionString = storageConnectionString;
            _wordDictionariesCacheService = wordDictionariesCacheService;
        }

        /// <summary>
        /// Retrieves the content of the dictionary.
        /// </summary>
        public WordDictionaries RetrieveFullDictionary()
        {
            // TODO Implement caching.
            // TODO Make sure this is thread-safe.  CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);

            if (_wordDictionariesCacheService.HasBeenPrimed())
            {
                return _wordDictionariesCacheService.RetrieveFromCache();
            }

            // Create the blob client.  
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);

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

            string[] contentSplit = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            WordDictionaries fullDictionary = new WordDictionaries(contentSplit);

            _wordDictionariesCacheService.SaveInCache(fullDictionary);

            return fullDictionary;
        }

    }
}