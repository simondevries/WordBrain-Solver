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
        /// Initiates the search
        /// </summary>
        void InitiateSearch(List<Point> visitedPoints, int x, int y, WordUnderInvestigation wordUnderInvestigation, char[,] board,
            List<WordUnderInvestigation> foundWords, Dictionary<string, List<string>> subDictionary);
    }
}