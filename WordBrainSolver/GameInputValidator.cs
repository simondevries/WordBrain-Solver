using System;
using System.Globalization;
using System.Linq;

namespace WordBrainSolver.Core
{
    public static class GameInputValidator
    {
        public static bool IsInputValid(int[] lookupWordLength, string inputBoard)
        {
            int result;
            bool isInteger = int.TryParse(Math.Sqrt(inputBoard.Length).ToString(CultureInfo.InvariantCulture), out result);
            int totalLookupWordLength = lookupWordLength.Sum();

            return lookupWordLength.Length != 0 && inputBoard.Length != 0 && lookupWordLength.Length < inputBoard.Length && isInteger && totalLookupWordLength == inputBoard.Length;
        }
    }
}