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
        public void Search(List<Point> visitedPoints, int lives, int x, int y, WordUnderInvestigation wordUnderInvestigation, char[,] board, List<WordUnderInvestigation> foundWords, Dictionary<string, IEnumerable<string>> subDictionary)
        {
            //Case 1- Goes off grid
            int boardLength = board.GetLength(0);
            if (x >= boardLength || x < 0 || y >= boardLength || y < 0)
            {
                return;
            }

            //Case 2 - Has been visited
            bool hasBeenVisited = visitedPoints.Any(point => point.HasValue(x, y));
            if (hasBeenVisited) return;

            wordUnderInvestigation.AddCharacter(board[x, y], new Point(x, y));

            //Case 3 - No possible word
            if (wordUnderInvestigation.HasLength(_bruteForceSearchLimit))
            {
                // If it is not in the sub dictionary then there is no need to continue
                _intelligentSecondaryWordSearcher.InitiateSearch(visitedPoints, x, y, wordUnderInvestigation, board,
                    foundWords, subDictionary);
                return;
            }

            visitedPoints.Add(new Point(x, y));

            //Case 4 - Ran out of lives
            if (lives == 1)
            {
                foundWords.Add(wordUnderInvestigation);
            }

            lives--;

            // todo get rid of gross cloning
            List<Point> clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedVisitedPoints, lives, x - 1, y - 1, new WordUnderInvestigation(wordUnderInvestigation), board, foundWords, subDictionary);

            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedVisitedPoints, lives, x, y - 1, new WordUnderInvestigation(wordUnderInvestigation), board, foundWords, subDictionary);

            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedVisitedPoints, lives, x + 1, y - 1, new WordUnderInvestigation(wordUnderInvestigation), board, foundWords, subDictionary);

            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedVisitedPoints, lives, x - 1, y, new WordUnderInvestigation(wordUnderInvestigation), board, foundWords, subDictionary);

            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedVisitedPoints, lives, x + 1, y, new WordUnderInvestigation(wordUnderInvestigation), board, foundWords, subDictionary);

            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedVisitedPoints, lives, x - 1, y + 1, new WordUnderInvestigation(wordUnderInvestigation), board, foundWords, subDictionary);

            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedVisitedPoints, lives, x, y + 1, new WordUnderInvestigation(wordUnderInvestigation), board, foundWords, subDictionary);

            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedVisitedPoints, lives, x + 1, y + 1, new WordUnderInvestigation(wordUnderInvestigation), board, foundWords, subDictionary);
            wordUnderInvestigation.RemoveLastCharacter();
        }
    }
}