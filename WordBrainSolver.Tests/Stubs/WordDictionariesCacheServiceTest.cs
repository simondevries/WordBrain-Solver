using WordBrainSolver.Core;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Tests.Stubs
{
    public class WordDictionariesCacheServiceStub : IWordDictionariesCacheService
    {
        public void SaveInCache(WordDictionaries fullDictionary)
        {
            throw new System.NotImplementedException();
        }

        public bool HasBeenPrimed()
        {
            return true;
        }

        public bool RetrieveWasCalled { get; set; }
        public WordDictionaries RetrieveFromCache()
        {
            RetrieveWasCalled = true;
            return new WordDictionaries(new []{"asd"});
        }
    }
}