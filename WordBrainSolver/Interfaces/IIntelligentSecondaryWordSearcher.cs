using System.Collections.Generic;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Able to generate a list of words that exist on a grid for a location.
    /// </summary>
    public interface IIntelligentSecondaryWordSearcher
    {
        /// <summary>
        /// Searches for words.
        /// Given an initial set of letters of length bruteForceSearchLimit, it is able to use a sub dictionary to check neighbouring tiles
        /// and identify if it is possible for make a word.
        /// </summary>
        bool Search(string possibleWord, List<Point> visitedPoints, char[,] board, int x, int y);
    }
}