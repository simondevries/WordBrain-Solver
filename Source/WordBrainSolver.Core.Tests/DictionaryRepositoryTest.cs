using System.Threading.Tasks;
using FluentAssertions;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;
using WordBrainSolver.Core.Tests.Builders;
using WordBrainSolver.Core.Tests.Stubs;
using Xunit;

namespace WordBrainSolver.Core.Tests
{
    public class DictionaryRepositoryTest
    {
        [Fact]
        public async Task CanLoadDictionary()
        {
            IDictionaryRepository dictionaryRepository = new DictionaryRepositoryBuilder().Build();

            WordDictionaries dictionary = await dictionaryRepository.RetrieveFullDictionary();

            dictionary.Should().NotBeNull();
            dictionary.IsFullDictionaryGenerated().Should().BeTrue();
        }

        [Fact]
        public async Task UsesCacheService()
        {
            WordDictionariesCacheServiceStub wordDictionariesCacheServiceStub = new WordDictionariesCacheServiceStub();
            IDictionaryRepository dictionaryRepository = new DictionaryRepositoryBuilder().With(wordDictionariesCacheServiceStub).Build();

            WordDictionaries dictionary = await dictionaryRepository.RetrieveFullDictionary();

            wordDictionariesCacheServiceStub.RetrieveWasCalled.Should().BeTrue();
            dictionary.Should().NotBeNull();
            dictionary.IsFullDictionaryGenerated().Should().BeTrue();
        }
    }
}