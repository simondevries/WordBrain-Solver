using System.Collections.Generic;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Responsible for managing words search and combination of words into final solutions.
    /// </summary>
    public interface IWordSearchCoordinator
    {
        /// <summary>
        /// Finds the solutions.
        /// </summary>
        List<string> FindSolutions(int wordLengthBeingSearchedFor, int gridSize, char[,] board);
    }
}