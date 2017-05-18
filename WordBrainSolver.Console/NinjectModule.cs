using System.Configuration;
using System.IO;
using WordBrainSolver.Core;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Console
{
    public class NinjectModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IGameCoordinator>().To<GameCoordinator>();
            Bind<IWordSearchCoordinator>().To<WordSearchCoordinator>();
            Bind<IWordSearcher>().To<WordSearcher>();
            Bind<ISubDictionaryGenerator>().To<SubDictionaryGenerator>();

            string relativeDictionaryPath = ConfigurationManager.AppSettings["RelativeDictionaryPath"];
            string fullDictionaryPath = Path.Combine(Directory.GetCurrentDirectory(), relativeDictionaryPath);
            Bind<IDictionaryRetriever>().To<FileDictionaryRetriever>().WithConstructorArgument(typeof(string), fullDictionaryPath);
            Bind<IDictionaryCoordinator>().To<DictionaryCoordinator>();
        }
    }
}