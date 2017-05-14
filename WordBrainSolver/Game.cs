using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordBrainSolver
{
    public class Game
    {
        private const string DictionaryFileName = "wordsSorted.csv";
        private const string ResourcesFolderName = "Resources";
        private string[] array;
        private List<string> wordsFound = new List<string>();
        private List<Point> visitedPoints = new List<Point>();
        private string[] dictionary;

        public List<string> RunGame(int lives, int gridSize, string inputBoard)
        {
            char[,] board = InitBoard(inputBoard, gridSize);

            GenerateFoundWords(lives, gridSize, board);

            wordsFound = SortAndDistinctList(wordsFound);

            GetDictionary();

            List<string> possibleWords = GetPossibleWords();

            possibleWords = SortAndDistinctList(possibleWords);

            foreach (string w in possibleWords)
            {
                Console.WriteLine("" + w);
            }

            return possibleWords;
        }

        private List<string> GetPossibleWords()
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

        private void GenerateFoundWords(int lives, int gridSize, char[,] board)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    FindWords(wordsFound, visitedPoints, lives, i, j, String.Empty, gridSize, board);
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

        private void GetDictionary()
        {
//            string dictionaryContent;
//            string dictionaryPath = Path.Combine(Directory.GetCurrentDirectory(), $@"{ResourcesFolderName}\{DictionaryFileName}");
//
//            if (!File.Exists(dictionaryPath))
//            {
//                throw new FileNotFoundException($"'{DictionaryFileName}' file was not found.");
//            }
//
//            using (StreamReader streamReader = new StreamReader(dictionaryPath))
//            {
//                dictionaryContent = streamReader.ReadToEnd();
//            }

            string dictionaryContent = new DictionaryRetriever().GetDictionary();

            dictionary = dictionaryContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }

        private void FindWords(List<string> wordsFound, List<Point> visitedPoints, int lives, int x, int y, string currentWord, int gridSize, char[,] board)
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

            //Case 3 - Ran out of lives
            if (lives == 1)
            {
                wordsFound.Add(currentWord);
            }

            lives--;

            // todo get rid of gross cloning
            List<Point> clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y - 1, currentWord, gridSize, board);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x, y - 1, currentWord, gridSize, board);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y - 1, currentWord, gridSize, board);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y, currentWord, gridSize, board);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y, currentWord, gridSize, board);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y + 1, currentWord, gridSize, board);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x, y + 1, currentWord, gridSize, board);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y + 1, currentWord, gridSize, board);

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