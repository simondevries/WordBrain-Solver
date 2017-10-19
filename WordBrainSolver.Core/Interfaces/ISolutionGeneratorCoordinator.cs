using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<List<string>> GenerateGameSolutions(int[] wordLengths, string inputBoard);
    }
}