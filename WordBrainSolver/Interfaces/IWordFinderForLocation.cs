using System.Collections.Generic;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Responsible for generating solutions for a given set of inputs in the game called: WordBrain.
    /// </summary>
    public interface IWordFinderForLocation
    {
        /// <summary>
        /// Generates the game's solutions.
        /// </summary>
        List<WordUnderInvestigation> FindWordsForLocation(int lives, int x, int y, char[,] board, WordDictionaries wordDictionaries);
    }
}