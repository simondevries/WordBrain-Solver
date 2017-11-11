using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Core.Dictionary;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;
using WordBrainSolver.Core.Tests.Builders;
using Xunit;

namespace WordBrainSolver.Core.Tests
{
    public class IntelligentSecondaryWordSearcherTest
    {
        private IntelligentSecondaryWordSearcher _intelligentSecondaryWordSearcher;
        private SubDictionaryGenerator _subDictionaryGenerator;
        private IDictionaryRepository _dictionaryRepository;


        public IntelligentSecondaryWordSearcherTest()
        {
            _intelligentSecondaryWordSearcher = new IntelligentSecondaryWordSearcher(3);
            _subDictionaryGenerator = new SubDictionaryGenerator(3);
            _dictionaryRepository = new DictionaryRepositoryBuilder().Build();
        }

        [Fact]
        public async void CanFindResults()
        {
            List<Point> visitedPoints = new List<Point>() { new Point(0, 0), new Point(1, 0) };
            int startX = 2;
            int startY = 1;

            WordUnderInvestigation wordUnderInvestigation = new WordUnderInvestigation("sen", new List<Point> { new Point(0, 1), new Point(1, 0), new Point(2, 1) }, 0);
            WordDictionaries wordDictionaries = await _dictionaryRepository.RetrieveFullDictionary();
            char[,] board = {
                { 'y','s','o','n'},
                { 'e','l','n','n'},
                { 'h','n','c','a'},
                { 'o','l','a','b'}
        };
            List<WordUnderInvestigation> foundwords = new List<WordUnderInvestigation>();
            Dictionary<string, IEnumerable<string>> subDictionaryForWord = _subDictionaryGenerator.RetrieveSubDictionaryForWord(5, wordDictionaries);
            _intelligentSecondaryWordSearcher.InitiateSearch(visitedPoints, startX, startY, wordUnderInvestigation, board, foundwords, subDictionaryForWord);

            foundwords.Count.Should().Be(1);
            foundwords.First().GetWord().Should().Be("senna");
        }
    }
}