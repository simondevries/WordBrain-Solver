using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordBrainSolver
{
    public class Game
    {
        private List<string> wordsFound = new List<string>();
        private SubDictionaryGenerator _subDictionaryGenerator;
        private DictionaryRetriever _dictionaryRetriever;
        private const int BrootForceSearchLimit = 3;

        public Game()
        {
            _subDictionaryGenerator = new SubDictionaryGenerator();
            _dictionaryRetriever = new DictionaryRetriever();
        }

        public List<string> RunGame(int lives, int gridSize, string inputBoard)
        {
            char[,] board = InitBoard(inputBoard, gridSize);

            string[] dictionary = GetDictionary();

            Dictionary<string, List<string>> subDictionary = _subDictionaryGenerator.GenerateSubDictionary(lives, dictionary, BrootForceSearchLimit);

            GenerateFoundWords(lives, gridSize, board, subDictionary);

            wordsFound = SortAndDistinctList(wordsFound);
            
            foreach (string w in wordsFound)
            {
                Console.WriteLine("" + w);
            }

            return wordsFound;
        }
        
        private void GenerateFoundWords(int lives, int gridSize, char[,] board, Dictionary<string, List<string>> subDictionary)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    FindWords(wordsFound, new List<Point>(), lives, i, j, string.Empty, board, subDictionary);
                }
            }
        }

        private void FindWords(List<string> wordsFound, List<Point> visitedPoints, int lives, int x, int y, string currentWord, char[,] board, Dictionary<string, List<string>> subDictionary)
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
            if (currentWord.Length == BrootForceSearchLimit)
            {
                if (subDictionary.ContainsKey(currentWord))
                {
                    foreach (string possibleWord in subDictionary[currentWord])
                    {

                        if (possibleWord == currentWord)
                        {
                            wordsFound.Add(possibleWord);
                            break;
                        }

                        string possibleWordTrimmed = possibleWord.Substring(BrootForceSearchLimit, possibleWord.Length - BrootForceSearchLimit);

                        bool found = SmartFindSurroundingWords(possibleWordTrimmed, visitedPoints,  board, x, y);

                        if (found)
                        {
                            wordsFound.Add(possibleWord);
                        }
                    }
                }

                return;
            }

            //Case 4 - Ran out of lives
            if (lives == 1)
            {
                wordsFound.Add(currentWord);
            }

            lives--;

            // todo get rid of gross cloning
            List<Point> clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y - 1, currentWord, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x, y - 1, currentWord, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y - 1, currentWord, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y, currentWord, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y, currentWord, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y + 1, currentWord, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x, y + 1, currentWord, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y + 1, currentWord, board, subDictionary);

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

        private char[,] InitBoard(string inputBoard, int gridSize)
        {
            char[,] outputBoard = new char[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    outputBoard[i, j] = Convert.ToChar(inputBoard.Substring(i * gridSize + j, 1));
                }
            }

            return outputBoard;
        }


        private List<string> SortAndDistinctList(List<string> list)
        {
            list.Sort();
            List<string> distictWords = new List<string>();
            foreach (string s in list.Distinct())
            {
                distictWords.Add(s);
            }
            return distictWords;
        }

        private string[] GetDictionary()
        {
            string dictionaryContent = _dictionaryRetriever.GetDictionary();

            return dictionaryContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
    }
}