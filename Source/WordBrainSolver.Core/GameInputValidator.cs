using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrainSolver.Core
{
    public static class GameInputValidator
    {
        public static bool IsInputValid(int[] wordLength, string board)
        {
            return !GetInputValidationErrors(wordLength, board).Any();
        }

        public static IEnumerable<string> GetInputValidationErrors(int[] wordLength, string board)
        {
            double squareRootOfBoardLength = Math.Sqrt(board.Length);

            if (board.Length == 0 || Math.Abs((int)squareRootOfBoardLength - squareRootOfBoardLength) > 0.0001)
            {
                yield return "Board length has to be a squared number. E.g. 4, 9, 16, ect";
            }

            if (wordLength.Length == 0 || wordLength.Sum() != board.Length)
            {
                yield return "Word length array must contain all word entries.";
            }
        }
    }
}