using System.Configuration;
using System.IO;
using WordBrainSolver.Core;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Tests
{
    public class GameCoordinatorBuilder
    {
        private IDictionaryRetriever _dictionaryRetriever;
        private readonly IDictionaryCoordinator _dictionaryCoordinator;
        private readonly IWordSearchCoordinator _wordSearchCoordinator;

        public GameCoordinatorBuilder()
        {
            WithFileDictionaryRetriever();
            ISubDictionaryGenerator subDictionaryGenerator = new SubDictionaryGenerator();
            _dictionaryCoordinator = new DictionaryCoordinator(subDictionaryGenerator, _dictionaryRetriever);
            IWordSearcher wordSearcher = new WordSearcher(_dictionaryCoordinator);
            _wordSearchCoordinator = new WordSearchCoordinator(wordSearcher);
        }

        public GameCoordinator Build()
        {
            return new GameCoordinator(_wordSearchCoordinator, _dictionaryCoordinator);
        }

        private GameCoordinatorBuilder WithFileDictionaryRetriever()
        {
            var relativeDictionaryPath = ConfigurationManager.AppSettings["RelativeDictionaryPath"];
            var fullDictionaryPath = Path.Combine(Directory.GetCurrentDirectory(), relativeDictionaryPath);
            _dictionaryRetriever = new FileDictionaryRetriever(fullDictionaryPath);
            return this;
        }
    }
}
