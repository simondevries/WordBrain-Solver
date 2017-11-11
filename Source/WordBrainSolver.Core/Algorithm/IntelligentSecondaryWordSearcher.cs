using System.Collections.Generic;
using System.Linq;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Algorithm
{
    /// <summary>
    /// Able to generate a list of words that exist on a grid for a location.
    /// </summary>
    public class IntelligentSecondaryWordSearcher : IIntelligentSecondaryWordSearcher
    {
        private readonly int _bruteForceSearchLimit;

        /// <summary>
        /// Constructor
        /// </summary>
        public IntelligentSecondaryWordSearcher(int bruteForceSearchLimit)
        {
            _bruteForceSearchLimit = bruteForceSearchLimit;
        }

        /// <summary>
        /// Initiates the search.
        /// Runs search on all words that start with the same three characters
        /// </summary>
        public void InitiateSearch(List<Point> visitedPoints, int x, int y, WordUnderInvestigation wordUnderInvestigation, char[,] board,
            List<WordUnderInvestigation> foundWords, Dictionary<string, IEnumerable<string>> subDictionary)
        {
            if (!wordUnderInvestigation.HasLength(_bruteForceSearchLimit))
            {
                return;
            }

            if (subDictionary.ContainsKey(wordUnderInvestigation.GetWord()))
            {
                foreach (string possibleWord in subDictionary[wordUnderInvestigation.GetWord()])
                {
                    WordUnderInvestigation wordToCheck = new WordUnderInvestigation(possibleWord, visitedPoints, _bruteForceSearchLimit - 1);
                    Search(wordToCheck, visitedPoints, board, x, y, foundWords);
                }
            }
        }

        /// <summary>
        /// Searches for words.
        /// Given an initial set of letters of length bruteForceSearchLimit, it is able to use a sub dictionary to check neighbouring tiles
        /// and identify if it is possible for make a word.
        /// </summary>
        public void Search(WordUnderInvestigation possibleWord, List<Point> visitedPoints, char[,] board, int x, int y, List<WordUnderInvestigation> foundWords)
        {
            //Case 1- goes off grid
            int boardLength = board.GetLength(0);
            if (x >= boardLength || x < 0 || y >= boardLength || y < 0)
            {
                return;
            }

            //Case 2- Position doesn't contain a value
            if (board[x, y] == '*') return;

            //Case 2-Already visited
            bool hasBeenVisited = visitedPoints.Any(point => point.HasValue(x, y));
            if (hasBeenVisited) return;

            //Case 3- not the next char in this word
            if (board[x, y] != possibleWord.GetCharAtCurrentSearchIndex())
            {
                return;
            }

            //Case 4- Success! No letters left in possible word
            if (possibleWord.IsCurrentSearchIndexAtEndOfWord())
            {
                visitedPoints.Add(new Point(x, y));
                possibleWord.SetVisitedPoints(visitedPoints.ToArray()); // NB: Must occur before increment index
                foundWords.Add(possibleWord);
                visitedPoints.RemoveAt(visitedPoints.Count - 1);
                return;
            }

            visitedPoints.Add(new Point(x, y));
            possibleWord.AddVisitedLocationAtCurrentSearchPosition(new Point(x, y), board[x, y]); // NB: Must occur before increment index
            possibleWord.IncrementCurrentSearchIndex();
            SmartFindSurroundingWords(possibleWord, visitedPoints, board, x, y, foundWords);
            visitedPoints.RemoveAt(visitedPoints.Count - 1);
        }

        private void SmartFindSurroundingWords(WordUnderInvestigation possibleWord, List<Point> visitedPoints, char[,] board, int x, int y, List<WordUnderInvestigation> foundWords)
        {
            Search(new WordUnderInvestigation(possibleWord), visitedPoints, board, x - 1, y - 1, foundWords);
            Search(new WordUnderInvestigation(possibleWord), visitedPoints, board, x, y - 1, foundWords);
            Search(new WordUnderInvestigation(possibleWord), visitedPoints, board, x + 1, y - 1, foundWords);
            Search(new WordUnderInvestigation(possibleWord), visitedPoints, board, x - 1, y, foundWords);
            Search(new WordUnderInvestigation(possibleWord), visitedPoints, board, x + 1, y, foundWords);
            Search(new WordUnderInvestigation(possibleWord), visitedPoints, board, x - 1, y + 1, foundWords);
            Search(new WordUnderInvestigation(possibleWord), visitedPoints, board, x, y + 1, foundWords);
            Search(new WordUnderInvestigation(possibleWord), visitedPoints, board, x + 1, y + 1, foundWords);
        }
    }
}