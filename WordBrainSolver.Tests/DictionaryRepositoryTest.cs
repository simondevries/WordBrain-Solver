using FluentAssertions;
using NUnit.Framework;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;
using WordBrainSolver.Tests.Builders;
using WordBrainSolver.Tests.Stubs;

namespace WordBrainSolver.Tests
{
    public class DictionaryRepositoryTest
    {
        [Test]
        public void CanLoadDictionary()
        {
            IDictionaryRepository dictionaryRepository = new DictionaryRepositoryBuilder().Build();

            WordDictionaries dictionary = dictionaryRepository.RetrieveFullDictionary();

            dictionary.Should().NotBeNull();
            dictionary.IsFullDictionaryGenerated().Should().BeTrue();
        }

        [Test]
        public void UsesCacheService()
        {
            WordDictionariesCacheServiceStub wordDictionariesCacheServiceStub = new WordDictionariesCacheServiceStub();
            IDictionaryRepository dictionaryRepository = new DictionaryRepositoryBuilder().With(wordDictionariesCacheServiceStub).Build();

            WordDictionaries dictionary = dictionaryRepository.RetrieveFullDictionary();

            wordDictionariesCacheServiceStub.RetrieveWasCalled.Should().BeTrue();
            dictionary.Should().NotBeNull();
            dictionary.IsFullDictionaryGenerated().Should().BeTrue();
        }
    }
}