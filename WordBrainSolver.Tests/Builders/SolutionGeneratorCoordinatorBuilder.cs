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
        private readonly IRemoveWordFromBoard _removeWordFromBoard;

        public SolutionGeneratorCoordinatorBuilder()
        {
            _removeWordFromBoard = new RemoveWordFromBoard();
            _dictionaryRepository = new DictionaryRepository(ConnectionStrings.AzureConnectionString, new WordDictionariesCacheService());
            _wordFinderForLocation = new WordFinderForLocation(new SubDictionaryGenerator(Settings.BruteForceSearchLimit), new BasicPrimaryWordSearcherBuilder().Build());
        }

        public SolutionGeneratorCoordinator Build()
        {
            return new SolutionGeneratorCoordinator(_dictionaryRepository, _wordFinderForLocation, new GameInputValidator(), _removeWordFromBoard);
        }
    }
}