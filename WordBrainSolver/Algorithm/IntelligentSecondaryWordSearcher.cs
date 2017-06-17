using System.Collections.Generic;
using System.Linq;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Algorithm
{
    /// <summary>
    /// Able to generate a list of words that exist on a grid for a location.
    /// </summary>
    public class IntelligentSecondaryWordSearcher : IIntelligentSecondaryWordSearcher
    {
        /// <summary>
        /// Searches for words.
        /// Given an initial set of letters of length bruteForceSearchLimit, it is able to use a sub dictionary to check neighbouring tiles
        /// and identify if it is possible for make a word.
        /// </summary>
        public bool Search(string possibleWord, List<Point> visitedPoints, char[,] board, int x, int y)
        {
            //Case 1- goes off grid
            int boardLength = board.GetLength(0);
            if (x >= boardLength || x < 0 || y >= boardLength || y < 0)
            {
                return false;
            }

            //Case 2-Already visited
            bool hasBeenVisited = visitedPoints.Any(point => point.X == x && point.Y == y);
            if (hasBeenVisited) return false;

            visitedPoints.Add(new Point { X = x, Y = y });


            //Case 3- no letters left in possible word
            if (possibleWord == string.Empty)
            {
                return true;
            }

            //Case 4- not the next char in this word
            char firstLetter = possibleWord[0];
            if (board[x, y] != firstLetter)
            {
                return false;
            }

            possibleWord = possibleWord.Substring(1);
            return SmartFindSurroundingWords(possibleWord, visitedPoints, board, x, y);
        }

        private bool SmartFindSurroundingWords(string possibleWord, List<Point> visitedPoints, char[,] board, int x, int y)
        {
            bool found = false;
            List<Point> clonedPoints = new List<Point>(visitedPoints);
            found |= Search(possibleWord, clonedPoints, board, x - 1, y - 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= Search(possibleWord, clonedPoints, board, x, y - 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= Search(possibleWord, clonedPoints, board, x + 1, y - 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= Search(possibleWord, clonedPoints, board, x - 1, y);
            clonedPoints = new List<Point>(visitedPoints);
            found |= Search(possibleWord, clonedPoints, board, x + 1, y);
            clonedPoints = new List<Point>(visitedPoints);
            found |= Search(possibleWord, clonedPoints, board, x - 1, y + 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= Search(possibleWord, clonedPoints, board, x, y + 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= Search(possibleWord, clonedPoints, board, x + 1, y + 1);

            return found;
        }
    }
}