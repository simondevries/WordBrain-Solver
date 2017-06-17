using System.Configuration;
using System.IO;
using WordBrainSolver.Core;
using WordBrainSolver.Core.Dictionary;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.SolutionGenerator;

namespace WordBrainSolver.Console
{
    public class NinjectModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<ISolutionGeneratorCoordinator>().To<SolutionGeneratorCoordinator>();
            Bind<IWordFinderForLocation>().To<WordFinderForLocation>();
            Bind<ISubDictionaryGenerator>().To<SubDictionaryGenerator>();

            string relativeDictionaryPath = ConfigurationManager.AppSettings["RelativeDictionaryPath"];
            string fullDictionaryPath = Path.Combine(Directory.GetCurrentDirectory(), relativeDictionaryPath);
            Bind<IDictionaryRetriever>().To<FileDictionaryRetriever>().WithConstructorArgument(typeof(string), fullDictionaryPath);
            Bind<IDictionaryCoordinator>().To<DictionaryCoordinator>();
        }
    }
}