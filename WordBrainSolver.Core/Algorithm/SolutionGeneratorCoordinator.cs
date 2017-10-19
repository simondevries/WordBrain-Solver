using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IRemoveWordFromBoard _removeWordFromBoard;
        private WordDictionaries _wordDictionaries;

        private readonly List<List<int>> _orderOfExecution = new List<List<int>>() {
            new List<int> { 0, 1, 2},
            new List<int> { 0, 1, 2},
            new List<int> { 1, 0, 2},
            new List<int> { 1, 2, 0},
            new List<int> { 2, 0, 1},
            new List<int> { 2, 1, 0},
        };


        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionGeneratorCoordinator"/> class.
        /// </summary>
        public SolutionGeneratorCoordinator(IDictionaryRepository dictionaryRepository, IWordFinderForLocation wordFinderForLocation,
            IRemoveWordFromBoard removeWordFromBoard)
        {
            _dictionaryRepository = dictionaryRepository;// ?? throw new ArgumentNullException(nameof(dictionaryCoordinator));
            _wordFinderForLocation = wordFinderForLocation;
            _removeWordFromBoard = removeWordFromBoard;
        }

        /// <summary>
        /// Generates all of the possible solutions to a puzzel
        /// </summary>
        public async Task<List<string>> GenerateGameSolutions(int[] wordLengths, string inputBoard)
        {
            if (!GameInputValidator.IsInputValid(wordLengths, inputBoard))
            {
                throw new WordBrainSolverException("Invalid Input");
            };
            _wordDictionaries = await _dictionaryRepository.RetrieveFullDictionary();
            char[,] board = InitializeBoard(inputBoard);
            var results = new List<string>();
            int boardSize = Convert.ToInt32(Math.Sqrt((double)inputBoard.Length));
            Generator(results, string.Empty, 0, 0, boardSize, wordLengths.ToList(), board);

            return results.Distinct().ToList();
        }

        private void Generator(List<string> results, string previouslyFoundWords, int orderOfExecutionCount, int orderOfExecution, int gridSize, List<int> wordLengths, char[,] inputBoard)
        {
            //If we have searched all then finish
            if (orderOfExecution == wordLengths.Count) return;

            if (orderOfExecutionCount == wordLengths.Count) return;

            //get the length of the word we are looking for
            int indexOf = _orderOfExecution[orderOfExecutionCount].IndexOf(orderOfExecution);
            int wordLength = wordLengths[indexOf];

            // Find the words with this lenght
            IEnumerable<WordUnderInvestigation> foundWords = GenerateGameSolutionsForGameState(wordLength, gridSize, inputBoard);

            //For each one found, search for remaining words after removing the found words from the board
            foreach (WordUnderInvestigation word in foundWords)
            {
                string previousWordWithCurrentWord = string.IsNullOrWhiteSpace(previouslyFoundWords) ? word.GetWord() : string.Concat(previouslyFoundWords, ", ", word.GetWord());
                // If there are no more words to search for then return the result
                if (orderOfExecution == wordLengths.Count - 1)
                {
                    results.Add(previousWordWithCurrentWord);
                    continue;
                }
                char[,] newBoard = _removeWordFromBoard.RemoveWords(inputBoard, word.GetVisitedLocations(), gridSize);

                //remove items from board
                Generator(results, previousWordWithCurrentWord, orderOfExecutionCount, orderOfExecution + 1, gridSize, wordLengths,
                    newBoard);
            }

            if (orderOfExecution == 0)
            {
                //Test next sub iteration
                orderOfExecution = 0;
                previouslyFoundWords = string.Empty;
                orderOfExecutionCount++;
                Generator(results, previouslyFoundWords, orderOfExecutionCount, orderOfExecution, gridSize, wordLengths, inputBoard);
            }
        }

        /// <summary>
        /// Generates the game's solutions fro a given state.
        /// </summary>
        public List<WordUnderInvestigation> GenerateGameSolutionsForGameState(int lives, int gridSize, char[,] board)
        {
            //todo sdv move this elsewhere            
            if (lives <= 0 || lives > board.Length * 2)
            {
                return null;
            }

            List<WordUnderInvestigation> solutions = new List<WordUnderInvestigation>();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    solutions.AddRange(_wordFinderForLocation.FindWordsForLocation(lives, i, j, board, _wordDictionaries));
                }
            }
            return solutions;
        }

        private char[,] InitializeBoard(string inputBoard)
        {
            int gridSize = Convert.ToInt32(Math.Sqrt((double)inputBoard.Length));

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
    }
}