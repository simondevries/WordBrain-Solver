using Microsoft.Extensions.Caching.Memory;
using WordBrainSolver.Core.Dictionary;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Core.Tests.Builders
{
    public class DictionaryRepositoryBuilder
    {
        private IWordDictionariesCacheService _wordDictionariesCacheService;


        public DictionaryRepositoryBuilder()
        {
            _wordDictionariesCacheService = new WordDictionariesCacheService(new MemoryCache(new MemoryCacheOptions()));
        }

        public DictionaryRepositoryBuilder With(IWordDictionariesCacheService wordDictionariesCacheService)
        {
            _wordDictionariesCacheService = wordDictionariesCacheService;
            return this;
        }

        public IDictionaryRepository Build()
        {
            return new DictionaryRepository(ConnectionStrings.AzureConnectionString, _wordDictionariesCacheService);
        }
    }
}
