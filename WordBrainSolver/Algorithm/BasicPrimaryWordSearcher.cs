using System.Collections.Generic;
using System.Linq;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Algorithm
{
    /// <summary>
    /// Able to generate a list of words that exist on a grid for a location.
    /// Once it reaches the bruteForceSeachLimit it changes over to the IntelligentSecondaryWordSearcher
    /// </summary>
    public class BasicPrimaryWordSearcher : IBasicPrimaryWordSearcher
    {
        private readonly IIntelligentSecondaryWordSearcher _intelligentSecondaryWordSearcher;
        private readonly int _bruteForceSearchLimit;

        /// <summary>
        /// Constructor
        /// </summary>
        public BasicPrimaryWordSearcher(IIntelligentSecondaryWordSearcher intelligentSecondaryWordSearcher, int bruteForceSearchLimit)
        {
            _intelligentSecondaryWordSearcher = intelligentSecondaryWordSearcher;
            _bruteForceSearchLimit = bruteForceSearchLimit;
        }

        /// <summary>
        /// Searches for words  
        /// Lives = letters remining in word
        /// </summary>
        public void Search(List<Point> visitedPoints, int lives, int x, int y, string currentWord, char[,] board, List<string> foundWords, Dictionary<string, List<string>> subDictionary)
        {
            //Case 1- Goes off grid
            int boardLength = board.GetLength(0);
            if (x >= boardLength || x < 0 || y >= boardLength || y < 0)
            {
                return;
            }

            //Case 2 - Has been visited
            bool hasBeenVisited = visitedPoints.Any(point => point.X == x && point.Y == y);
            if (hasBeenVisited) return;

            visitedPoints.Add(new Point { X = x, Y = y });
            currentWord = currentWord + board[x, y];

            //Case 3 - No possible word
            if (TryUseIntelligentAlgorithm(visitedPoints, x, y, currentWord, board, foundWords, subDictionary)) return;

            //Case 4 - Ran out of lives
            if (lives == 1)
            {
                foundWords.Add(currentWord);
            }

            lives--;

            // todo get rid of gross cloning
            List<Point> clonedPoints = new List<Point>(visitedPoints);
            Search(clonedPoints, lives, x - 1, y - 1, currentWord, board, foundWords, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            Search(clonedPoints, lives, x, y - 1, currentWord, board, foundWords, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            Search(clonedPoints, lives, x + 1, y - 1, currentWord, board, foundWords, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            Search(clonedPoints, lives, x - 1, y, currentWord, board, foundWords, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            Search(clonedPoints, lives, x + 1, y, currentWord, board, foundWords, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            Search(clonedPoints, lives, x - 1, y + 1, currentWord, board, foundWords, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            Search(clonedPoints, lives, x, y + 1, currentWord, board, foundWords, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            Search(clonedPoints, lives, x + 1, y + 1, currentWord, board, foundWords, subDictionary);

            currentWord.Remove(currentWord.Length - 1);
        }

        /// <summary>
        /// Checks whether the intelligent algorithm can be used
        /// </summary>
        private bool TryUseIntelligentAlgorithm(List<Point> visitedPoints, int x, int y, string currentWord, char[,] board,
            List<string> foundWords, Dictionary<string, List<string>> subDictionary)
        {
            if (currentWord.Length != _bruteForceSearchLimit)
            {
                return false;
            }
            if (!subDictionary.ContainsKey(currentWord))
            {
                return true;
            }
            foreach (string possibleWord in subDictionary[currentWord])
            {
                if (possibleWord == currentWord)
                {
                    foundWords.Add(possibleWord);
                    break;
                }

                string possibleWordTrimmed = possibleWord.Substring(_bruteForceSearchLimit,
                    possibleWord.Length - _bruteForceSearchLimit);

                bool found = _intelligentSecondaryWordSearcher.Search(possibleWordTrimmed, visitedPoints, board, x, y);

                if (found)
                {
                    foundWords.Add(possibleWord);
                }
            }
            return true;
        }
    }
}