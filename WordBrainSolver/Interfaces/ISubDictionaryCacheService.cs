using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Word Dictionary Cache service
    /// </summary>
    public interface IWordDictionariesCacheService
    {
        /// <summary>
        /// Saves the word dictionary to the cache
        /// </summary>
        void SaveInCache(WordDictionaries fullDictionary);

        /// <summary>
        /// Returns whether the cache has been primed
        /// </summary>
        /// <returns></returns>
        bool HasBeenPrimed();

        /// <summary>
        /// Gets the word dictionaries object from Cache
        /// </summary>
        /// <returns></returns>
        WordDictionaries RetrieveFromCache();
    }
}