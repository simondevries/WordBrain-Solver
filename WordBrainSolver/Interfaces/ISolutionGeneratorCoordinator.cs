using System.Collections.Generic;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Responsible for generating solutions for a given set of inputs in the game called: WordBrain.
    /// </summary>
    public interface ISolutionGeneratorCoordinator
    {
        /// <summary>
        /// Generates the game's solutions.
        /// </summary>
        List<string> GenerateGameSolutions(int lives, int gridSize, string inputBoard);
    }
}