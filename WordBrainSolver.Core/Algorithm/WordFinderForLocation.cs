using System.Collections.Generic;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Algorithm
{
    /// <summary>
    /// Finds words that exist on the board for a given start location and word length.
    /// </summary>
    public class WordFinderForLocation : IWordFinderForLocation
    {
        private readonly ISubDictionaryGenerator _subDictionaryCoordinator;
        private readonly IBasicPrimaryWordSearcher _basicPrimaryWordSearcher;

        /// <summary>
        /// Constructor
        /// </summary>
        public WordFinderForLocation(ISubDictionaryGenerator subDictionaryCoordinator,
            IBasicPrimaryWordSearcher basicPrimaryWordSearcher)
        {
            _subDictionaryCoordinator = subDictionaryCoordinator;
            // ?? throw new ArgumentNullException(nameof(dictionaryCoordinator));
            _basicPrimaryWordSearcher = basicPrimaryWordSearcher;
        }

        /// <summary>
        /// Finds words of a specific length on specified location on the board.
        /// </summary>
        public List<WordUnderInvestigation> FindWordsForLocation(int wordLengthBeingSearchedFor, int x, int y, char[,] board, WordDictionaries wordDictionaries)
        {
            List<WordUnderInvestigation> foundWords = new List<WordUnderInvestigation>();
            Dictionary<string, IEnumerable<string>> subDictionary =
                _subDictionaryCoordinator.RetrieveSubDictionaryForWord(wordLengthBeingSearchedFor, wordDictionaries);
            _basicPrimaryWordSearcher.Search(new List<Point>(), wordLengthBeingSearchedFor, x, y, new WordUnderInvestigation(), board,
                foundWords, subDictionary);

            return foundWords;
        }
    }
}