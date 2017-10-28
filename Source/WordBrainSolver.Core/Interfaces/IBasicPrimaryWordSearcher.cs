using System.Collections.Generic;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Able to generate a list of words that exist on a grid for a location.
    /// Once it reaches the bruteForceSeachLimit it changes over to the IntelligentSecondaryWordSearcher
    /// </summary>
    public interface IBasicPrimaryWordSearcher
    {
        /// <summary>
        /// Searches for words
        /// </summary>
        void Search(List<Point> visitedPoints, int lives, int x, int y, WordUnderInvestigation wordUnderInvestigation, char[,] board, List<WordUnderInvestigation> foundWords, Dictionary<string, IEnumerable<string>> subDictionary);
    }
}