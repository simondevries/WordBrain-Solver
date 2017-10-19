using System;
using Microsoft.Extensions.Caching.Memory;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core
{
    /// <summary>
    /// Word Dictionary Cache service
    /// </summary>
    public class WordDictionariesCacheService : IWordDictionariesCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private const int CacheExpiryTime = 10;
        private const string CacheName = "FullDictionary";

        /// <summary>
        /// Constructor
        /// </summary>
        public WordDictionariesCacheService()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        /// <summary>
        /// Saves the word dictionary to the cache
        /// </summary>
        public void SaveInCache(WordDictionaries fullDictionary)
        {
            var expiration = DateTimeOffset.UtcNow.AddHours(CacheExpiryTime);

            _memoryCache.Set(CacheName, fullDictionary, expiration);
        }

        /// <summary>
        /// Returns whether the cache has been primed
        /// </summary>
        /// <returns></returns>
        public bool HasBeenPrimed()
        {
            return _memoryCache.TryGetValue(CacheName, out WordDictionaries _);
        }

        /// <summary>
        /// Gets the word dictionaries object from Cache
        /// </summary>
        /// <returns></returns>
        public WordDictionaries RetrieveFromCache()
        {
            if (_memoryCache.TryGetValue(CacheName, out WordDictionaries dictionaries))
            {
                return dictionaries;
            }

            return null;
        }
    }
}