using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordBrainSolver
{
    public class Game
    {
        private List<string> wordsFound = new List<string>();
        private List<Point> visitedPoints = new List<Point>();
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

            List<string> possibleWords = GetPossibleWords(dictionary);

            possibleWords = SortAndDistinctList(possibleWords);

            foreach (string w in possibleWords)
            {
                Console.WriteLine("" + w);
            }

            return possibleWords;
        }

        private List<string> GetPossibleWords(string[] dictionary)
        {
            List<string> possibleWords = new List<string>();
            int i = 0;
            foreach (string wordFound in wordsFound)
            {
                string w = wordFound.ToLower();
                for (; i < dictionary.Length; i++)
                {
                    int result = string.Compare(w, dictionary[i]);
                    if (result < 0)
                    {
                        //Not in dictionary                     
                        break;
                    }
                    if (result == 0)
                    {
                        //Found
                        possibleWords.Add(dictionary[i]);
                        break;
                    }
                    if (result > 0)
                    {
                        //Keep searching dictionary
                    }
                }
            }
            return possibleWords;
        }

        private void GenerateFoundWords(int lives, int gridSize, char[,] board, Dictionary<string, List<string>> subDictionary)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    FindWords(wordsFound, visitedPoints, lives, i, j, string.Empty, gridSize, board, subDictionary);
                    visitedPoints.Clear();
                }
            }
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

        private void FindWords(List<string> wordsFound, List<Point> visitedPoints, int lives, int x, int y, string currentWord, int gridSize, char[,] board, Dictionary<string, List<string>> subDictionary)
        {
            //Case 1- Goes off grid
            if (x >= gridSize || x < 0 || y >= gridSize || y < 0)
            {
                return;
            }

            //Case 2 - Has been visited
            bool hasBeenVisited = visitedPoints.Any(point => point.X == x && point.Y == y);
            if (hasBeenVisited) return;

            visitedPoints.Add(new Point { X = x, Y = y });
            currentWord = currentWord + board[x, y];

            if (currentWord.Length == BrootForceSearchLimit)
            {
                if (!subDictionary.ContainsKey(currentWord))
                {
                    return;
                }
            }

            //Case 3 - Ran out of lives
            if (lives == 1)
            {
                wordsFound.Add(currentWord);
            }

            lives--;

            // todo get rid of gross cloning
            List<Point> clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y - 1, currentWord, gridSize, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x, y - 1, currentWord, gridSize, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y - 1, currentWord, gridSize, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y, currentWord, gridSize, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y, currentWord, gridSize, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y + 1, currentWord, gridSize, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x, y + 1, currentWord, gridSize, board, subDictionary);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y + 1, currentWord, gridSize, board, subDictionary);

            currentWord.Remove(currentWord.Length - 1);
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
    }
}