//using System.Configuration;
//using System.IO;
//using WordBrainSolver.Core;
//using WordBrainSolver.Core.Dictionary;
//using WordBrainSolver.Core.Interfaces;
//using WordBrainSolver.Core.SolutionGenerator;
//
//namespace WordBrainSolver.Tests
//{
//    public class GameCoordinatorBuilder
//    {
//        private IDictionaryRetriever _dictionaryRetriever;
//        private readonly IDictionaryCoordinator _dictionaryCoordinator;
//        private readonly IWordFinderForLocation _wordsearcher;
//
//
//        public GameCoordinatorBuilder()
//        {
//            WithFileDictionaryRetriever();
//            ISubDictionaryGenerator subDictionaryGenerator = new SubDictionaryGenerator();
//            _dictionaryCoordinator = new DictionaryCoordinator(subDictionaryGenerator, _dictionaryRetriever);
//        }
//
//        public SolutionGeneratorCoordinator Build()
//        {
//            return new SolutionGeneratorCoordinator(_wordsearcher, _dictionaryCoordinator);
//        }
//
//        private GameCoordinatorBuilder WithFileDictionaryRetriever()
//        {
//            var relativeDictionaryPath = ConfigurationManager.AppSettings["RelativeDictionaryPath"];
//            var fullDictionaryPath = Path.Combine(Directory.GetCurrentDirectory(), relativeDictionaryPath);
//            _dictionaryRetriever = new FileDictionaryRetriever(fullDictionaryPath);
//            return this;
//        }
//    }
//}
