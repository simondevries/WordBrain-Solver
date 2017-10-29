using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Algorithm for removing words
    /// </summary>
    public interface IRemoveWordFromBoard
    {
        /// <summary>
        /// Removes a word from the board and then moves shuffles everything down according to gravity
        /// </summary>
        char[,] RemoveWords(char[,] inputBoard, Point[] positionsToRemove, int gridSize);
    }
}