﻿using Microsoft.Extensions.Caching.Memory;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Core.Dictionary;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Core.Tests.Builders
{
    class SolutionGeneratorCoordinatorBuilder
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly IWordFinderForLocation _wordFinderForLocation;
        private readonly IRemoveWordFromBoard _removeWordFromBoard;

        public SolutionGeneratorCoordinatorBuilder()
        {
            _removeWordFromBoard = new RemoveWordFromBoard();
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            _dictionaryRepository = new DictionaryRepository(ConnectionStrings.AzureConnectionString, new WordDictionariesCacheService(memoryCache));
            _wordFinderForLocation = new WordFinderForLocation(new SubDictionaryGenerator(Settings.BruteForceSearchLimit), new BasicPrimaryWordSearcherBuilder().Build());
        }

        public SolutionGeneratorCoordinator Build()
        {
            return new SolutionGeneratorCoordinator(_dictionaryRepository, _wordFinderForLocation, _removeWordFromBoard);
        }
    }
}