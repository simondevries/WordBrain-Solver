using WordBrainSolver.Core;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Core.Dictionary;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Tests.Builders
{
    class SolutionGeneratorCoordinatorBuilder
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly IWordFinderForLocation _wordFinderForLocation;
        private int _bruteForceSearchLimit;

        public SolutionGeneratorCoordinatorBuilder()
        {
            _bruteForceSearchLimit = 3;
            _dictionaryRepository = new DictionaryRepository(ConnectionStrings.AzureConnectionString, new WordDictionariesCacheService());
            _wordFinderForLocation = new WordFinderForLocation(new SubDictionaryGenerator(_bruteForceSearchLimit), new BasicPrimaryWordSearcher(new IntelligentSecondaryWordSearcher(), _bruteForceSearchLimit));
        }

        public SolutionGeneratorCoordinator Build()
        {
            return new SolutionGeneratorCoordinator(_dictionaryRepository, _wordFinderForLocation, new GameInputValidator());
        }
    }
}