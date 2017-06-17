﻿using WordBrainSolver.Core;
using WordBrainSolver.Core.Dictionary;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Tests.Builders
{
    public class DictionaryRepositoryBuilder
    {
        private IWordDictionariesCacheService _wordDictionariesCacheService;


        public DictionaryRepositoryBuilder()
        {
            _wordDictionariesCacheService = new WordDictionariesCacheService();
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
