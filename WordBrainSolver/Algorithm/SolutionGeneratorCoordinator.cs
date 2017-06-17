using System.Collections.Generic;
using System.Linq;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Algorithm
{
    /// <summary>
    /// Responsible for generating solutions for a given set of inputs in the game called: WordBrain.
    /// </summary>
    public class SolutionGeneratorCoordinator : ISolutionGeneratorCoordinator
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly IWordFinderForLocation _wordFinderForLocation;
        private readonly IGameInputValidator _gameInputValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionGeneratorCoordinator"/> class.
        /// </summary>
        public SolutionGeneratorCoordinator(IDictionaryRepository dictionaryRepository, IWordFinderForLocation wordFinderForLocation, IGameInputValidator gameInputValidator)
        {
            _dictionaryRepository = dictionaryRepository;// ?? throw new ArgumentNullException(nameof(dictionaryCoordinator));
            _wordFinderForLocation = wordFinderForLocation;
            _gameInputValidator = gameInputValidator;
        }

        /// <summary>
        /// Generates the game's solutions.
        /// </summary>
        public List<string> GenerateGameSolutions(int lives, int gridSize, string inputBoard)
        {
            bool isInputValid = _gameInputValidator.Validate(lives, gridSize, inputBoard);
            if (!isInputValid) return null;

            char[,] board = InitializeBoard(inputBoard, gridSize);
            WordDictionaries wordDictionaries = _dictionaryRepository.RetrieveFullDictionary();
            List<string> solutions = new List<string>();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    solutions.AddRange(_wordFinderForLocation.FindWordsForLocation(new List<Point>(), lives, i, j, string.Empty, board, wordDictionaries));
                }
            }
            return SortAndDistinctList(solutions);
        }

        private char[,] InitializeBoard(string inputBoard, int gridSize)
        {
            char[,] outputBoard = new char[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    outputBoard[i, j] = inputBoard.ElementAt(i * gridSize + j);
                }
            }

            return outputBoard;
        }

        private List<string> SortAndDistinctList(List<string> list)
        {
            list.Sort();
            List<string> distinctWords = new List<string>();
            foreach (string s in list.Distinct())
            {
                distinctWords.Add(s);
            }
            return distinctWords;
        }
    }
}