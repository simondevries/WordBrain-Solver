using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordBrainSolver
{
    class Game
    {
        private const string DictionaryFileName = "wordsSorted.csv";
        private const string ResourcesFolderName = "Resources";
        private string[] array; //= { "HFE", "RLR", "USI" };
        private char[,] board;// = new char[GRID_SIZE, GRID_SIZE];
        private List<string> wordsFound = new List<string>();
        private List<Point> visitedPoints = new List<Point>();
        private string[] dictionary;

        public Game()
        {
            Console.WriteLine("Enter Grid Size, for example '3' or '4'");
            int gridSize = Convert.ToInt32(Console.ReadLine());
            board = new char[gridSize, gridSize];
            array = new string[gridSize];

            Console.WriteLine("Enter Board from left to right top to bottom");
            string readLine = Console.ReadLine();

            for (int i = 0; i < gridSize; i++)
            {
                array[i] = readLine.Substring(i*gridSize, gridSize);
            }

            Console.WriteLine("Enter Word Length");
            int lives = Convert.ToInt32(Console.ReadLine());

            RunGame(lives, gridSize);
        }

        public void RunGame(int lives, int gridSize)
        {
            InitBoard();

            GenerateFoundWords(lives, gridSize);

            wordsFound = SortAndDistinctList(wordsFound);

            GetDictionary();

            List<string> possibleWords = GetPossibleWords();

            possibleWords = SortAndDistinctList(possibleWords);

            foreach (string w in possibleWords)
            {
                Console.WriteLine("" + w);
            }
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

        private void GenerateFoundWords(int lives, int gridSize)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    FindWords(wordsFound, visitedPoints, lives, i, j, String.Empty, gridSize);
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
            string dictionaryContent;
            string dictionaryPath = Path.Combine(Directory.GetCurrentDirectory(), $@"{ResourcesFolderName}\{DictionaryFileName}");

            if (!File.Exists(dictionaryPath))
            {
                throw new FileNotFoundException($"'{DictionaryFileName}' file was not found.");
            }

            using (StreamReader streamReader = new StreamReader(dictionaryPath))
            {
                dictionaryContent = streamReader.ReadToEnd();
            }
            dictionary = dictionaryContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }

        private void FindWords(List<string> wordsFound, List<Point> visitedPoints, int lives, int x, int y, string currentWord, int gridSize)
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
            FindWords(wordsFound, clonedPoints, lives, x - 1, y - 1, currentWord, gridSize);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x, y - 1, currentWord, gridSize);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y - 1, currentWord, gridSize);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y, currentWord, gridSize);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y, currentWord, gridSize);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x - 1, y + 1, currentWord, gridSize);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x, y + 1, currentWord, gridSize);
            clonedPoints = new List<Point>(visitedPoints);
            FindWords(wordsFound, clonedPoints, lives, x + 1, y + 1, currentWord, gridSize);

            currentWord.Remove(currentWord.Length - 1);
        }

        private void InitBoard()
        {

            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array[i].Length; j++)
                {
                    board[i, j] = array[i][j];
                }
        }
    }
}