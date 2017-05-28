using System;
using System.Collections.Generic;
using System.Linq;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Core
{
    /// <summary>
    /// Responsible for managing words search and combination of words into final solutions.
    /// </summary>
    public class WordSearchCoordinator : IWordSearchCoordinator
    {
        private readonly IWordSearcher _wordSearcher;
        private readonly List<string> _wordsFound = new List<string>();

        public WordSearchCoordinator(IWordSearcher wordSearcher)
        {
            _wordSearcher = wordSearcher ?? throw new ArgumentNullException(nameof(wordSearcher));
        }

        /// <summary>
        /// Finds the solutions.
        /// </summary>
        public List<string> FindSolutions(int wordLengthBeingSearchedFor, int gridSize, char[,] board)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    _wordsFound.AddRange(_wordSearcher.FindWords(new List<Point>(), wordLengthBeingSearchedFor, i, j, string.Empty, board));
                }
            }
            return _wordsFound;
        }
    }
}