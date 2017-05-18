using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrainSolver.Core.Interfaces
{
    public class WordSearcher : IWordSearcher
    {
        private readonly IDictionaryCoordinator _dictionaryCoordinator;
        private const int BrutForceSearchLimit = 3;
        private Dictionary<string, List<string>> _subDictionary;
        private readonly List<string> _foundWords;

        public WordSearcher(IDictionaryCoordinator dictionaryCoordinator)
        {
            _dictionaryCoordinator = dictionaryCoordinator ?? throw new ArgumentNullException(nameof(dictionaryCoordinator));
            _foundWords = new List<string>();
        }

        public List<string> FindWords(List<Point> visitedPoints, int wordLengthBeingSearchedFor, int x, int y, string currentWord, char[,] board)
        {
            _subDictionary = _dictionaryCoordinator.GenerateSubDictionary(BrutForceSearchLimit, wordLengthBeingSearchedFor);
            RecursiveFind(visitedPoints, wordLengthBeingSearchedFor, x, y, currentWord, board);
            return _foundWords;
        }

        private void RecursiveFind(List<Point> visitedPoints, int lives, int x, int y, string currentWord, char[,] board)
        {
            //Case 1- Goes off grid
            int boardLength = board.GetLength(0);
            if (x >= boardLength || x < 0 || y >= boardLength || y < 0)
            {
                return;
            }

            //Case 2 - Has been visited
            bool hasBeenVisited = visitedPoints.Any(point => point.X == x && point.Y == y);
            if (hasBeenVisited) return;

            visitedPoints.Add(new Point { X = x, Y = y });
            currentWord = currentWord + board[x, y];

            //Case 3 - No possible word
            if (currentWord.Length == BrutForceSearchLimit)
            {
                if (_subDictionary.ContainsKey(currentWord))
                {
                    foreach (string possibleWord in _subDictionary[currentWord])
                    {

                        if (possibleWord == currentWord)
                        {
                            _foundWords.Add(possibleWord);
                            break;
                        }

                        string possibleWordTrimmed = possibleWord.Substring(BrutForceSearchLimit, possibleWord.Length - BrutForceSearchLimit);

                        bool found = SmartFindSurroundingWords(possibleWordTrimmed, visitedPoints, board, x, y);

                        if (found)
                        {
                            _foundWords.Add(possibleWord);
                        }
                    }
                }

                return;
            }

            //Case 4 - Ran out of lives
            if (lives == 1)
            {
                _foundWords.Add(currentWord);
            }

            lives--;

            // todo get rid of gross cloning
            List<Point> clonedPoints = new List<Point>(visitedPoints);
            RecursiveFind(clonedPoints, lives, x - 1, y - 1, currentWord, board);
            clonedPoints = new List<Point>(visitedPoints);
            RecursiveFind(clonedPoints, lives, x, y - 1, currentWord, board);
            clonedPoints = new List<Point>(visitedPoints);
            RecursiveFind(clonedPoints, lives, x + 1, y - 1, currentWord, board);
            clonedPoints = new List<Point>(visitedPoints);
            RecursiveFind(clonedPoints, lives, x - 1, y, currentWord, board);
            clonedPoints = new List<Point>(visitedPoints);
            RecursiveFind(clonedPoints, lives, x + 1, y, currentWord, board);
            clonedPoints = new List<Point>(visitedPoints);
            RecursiveFind(clonedPoints, lives, x - 1, y + 1, currentWord, board);
            clonedPoints = new List<Point>(visitedPoints);
            RecursiveFind(clonedPoints, lives, x, y + 1, currentWord, board);
            clonedPoints = new List<Point>(visitedPoints);
            RecursiveFind(clonedPoints, lives, x + 1, y + 1, currentWord, board);

            currentWord.Remove(currentWord.Length - 1);
        }

        private bool SmartFindWords(string possibleWord, List<Point> visitedPoints, char[,] board, int x, int y)
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
            found |= SmartFindWords(possibleWord, clonedPoints, board, x - 1, y - 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= SmartFindWords(possibleWord, clonedPoints, board, x, y - 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= SmartFindWords(possibleWord, clonedPoints, board, x + 1, y - 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= SmartFindWords(possibleWord, clonedPoints, board, x - 1, y);
            clonedPoints = new List<Point>(visitedPoints);
            found |= SmartFindWords(possibleWord, clonedPoints, board, x + 1, y);
            clonedPoints = new List<Point>(visitedPoints);
            found |= SmartFindWords(possibleWord, clonedPoints, board, x - 1, y + 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= SmartFindWords(possibleWord, clonedPoints, board, x, y + 1);
            clonedPoints = new List<Point>(visitedPoints);
            found |= SmartFindWords(possibleWord, clonedPoints, board, x + 1, y + 1);
            clonedPoints = new List<Point>(visitedPoints);

            return found;
        }
    }
}