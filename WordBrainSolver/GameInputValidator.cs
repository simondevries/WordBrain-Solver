using System;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Core
{
    public class GameInputValidator : IGameInputValidator
    {
        public bool Validate(int lookupWordLength, int gridSize, char[,] boardInput)
        {
            if (lookupWordLength <= 0 || lookupWordLength > boardInput.Length)
            {
                return false;
            }

            var gridSizeBasedOnBoardInput = Math.Sqrt(boardInput.Length);
            if (Math.Abs(gridSizeBasedOnBoardInput % 1) <= (double.Epsilon * 100) && (int)gridSizeBasedOnBoardInput != gridSize)
            {
                return false;
            }

            return true;
        }
    }
}