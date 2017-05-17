using System;
using System.Collections.Generic;
using System.Linq;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Core
{
    /// <summary>
    /// Responsible for generating solutions for a given set of inputs in the game called: WordBrain.
    /// </summary>
    public class GameCoordinator : IGameCoordinator
    {
        private readonly IDictionaryCoordinator _dictionaryCoordinator;
        private readonly IWordSearchCoordinator _wordSearchCoordinator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameCoordinator"/> class.
        /// </summary>
        public GameCoordinator(IWordSearchCoordinator wordSearchCoordinator, IDictionaryCoordinator dictionaryCoordinator)
        {
            _wordSearchCoordinator = wordSearchCoordinator ?? throw new ArgumentNullException(nameof(wordSearchCoordinator));
            _dictionaryCoordinator = dictionaryCoordinator ?? throw new ArgumentNullException(nameof(dictionaryCoordinator));
        }

        /// <summary>
        /// Generates the game's solutions.
        /// </summary>
        public List<string> GenerateGameSolutions(int lives, int gridSize, string inputBoard)
        {
            bool isInputValid = new GameInputValidator().Validate(lives, gridSize, inputBoard);
            if (!isInputValid) return null;

            char[,] board = InitBoard(inputBoard, gridSize);
            _dictionaryCoordinator.RetrieveDictionaryContent();
            List<string> solutions = _wordSearchCoordinator.FindSolutions(lives, gridSize, board);
            return SortAndDistinctList(solutions);
        }

        private char[,] InitBoard(string inputBoard, int gridSize)
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