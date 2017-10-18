using System;
using System.Runtime.Caching;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core
{
    /// <summary>
    /// Word Dictionary Cache service
    /// </summary>
    public class WordDictionariesCacheService : IWordDictionariesCacheService
    {
        private readonly MemoryCache _memoryCache;
        private const int CACHEEXPIRYTIME = 10;
        private const string CACHENAME = "FullDictionary";

        /// <summary>
        /// Constructor
        /// </summary>
        public WordDictionariesCacheService()
        {
            _memoryCache = MemoryCache.Default;
        }

        /// <summary>
        /// Saves the word dictionary to the cache
        /// </summary>
        public void SaveInCache(WordDictionaries fullDictionary)
        {
            var expiration = DateTimeOffset.UtcNow.AddHours(CACHEEXPIRYTIME);

            _memoryCache.Add(CACHENAME, fullDictionary, expiration);
        }

        /// <summary>
        /// Returns whether the cache has been primed
        /// </summary>
        /// <returns></returns>
        public bool HasBeenPrimed()
        {
            return _memoryCache.Contains(CACHENAME);
        }

        /// <summary>
        /// Gets the word dictionaries object from Cache
        /// </summary>
        /// <returns></returns>
        public WordDictionaries RetrieveFromCache()
        {
            if (_memoryCache.Contains(CACHENAME))
            {
                return (WordDictionaries)_memoryCache.Get(CACHENAME);
            }

            return null;
        }
    }
}