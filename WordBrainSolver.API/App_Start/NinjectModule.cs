using System;
using System.Configuration;
using WordBrainSolver.Core;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Core.Dictionary;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.API
{
    public class NinjectModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            int bruteForceSearchLimit = Convert.ToInt32(ConfigurationManager.AppSettings["bruteForceSearchLimit"]);
            string storageConnectionString = ConfigurationManager.AppSettings["storageConnectionString"];
            Bind<ISolutionGeneratorCoordinator>().To<SolutionGeneratorCoordinator>();
            Bind<IWordFinderForLocation>().To<WordFinderForLocation>();
            Bind<ISubDictionaryGenerator>().To<SubDictionaryGenerator>().WithConstructorArgument("bruteForceSearchLimit", bruteForceSearchLimit);
            Bind<IDictionaryRepository>().To<DictionaryRepository>().WithConstructorArgument(typeof(string), storageConnectionString);
            Bind<IGameInputValidator>().To<GameInputValidator>();
            Bind<IBasicPrimaryWordSearcher>().To<BasicPrimaryWordSearcher>().WithConstructorArgument("bruteForceSearchLimit", bruteForceSearchLimit);
            Bind<IIntelligentSecondaryWordSearcher>().To<IntelligentSecondaryWordSearcher>().WithConstructorArgument("bruteForceSearchLimit", bruteForceSearchLimit);
            Bind<IWordDictionariesCacheService>().To<WordDictionariesCacheService>();
            Bind<IRemoveWordFromBoard>().To<RemoveWordFromBoard>();
        }
    }
}