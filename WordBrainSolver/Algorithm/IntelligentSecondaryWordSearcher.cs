﻿using System.Collections.Generic;
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
        private readonly int _bruteForceSearchLimit;

        /// <summary>
        /// Constructor
        /// </summary>
        public IntelligentSecondaryWordSearcher(int bruteForceSearchLimit)
        {
            _bruteForceSearchLimit = bruteForceSearchLimit;
        }

        /// <summary>
        /// Initiates the search.
        /// Runs search on all words that start with the same three characters
        /// </summary>
        public void InitiateSearch(List<Point> visitedPoints, int x, int y, WordUnderInvestigation wordUnderInvestigation, char[,] board,
            List<WordUnderInvestigation> foundWords, Dictionary<string, List<string>> subDictionary)
        {
            if (!wordUnderInvestigation.HasLength(_bruteForceSearchLimit))
            {
                return;
            }

            if (subDictionary.ContainsKey(wordUnderInvestigation.GetWord()))
            {
                foreach (string possibleWord in subDictionary[wordUnderInvestigation.GetWord()])
                {
                    //Not sure if this deep clone is required but ill just be safe
                    List<Point> visitedLocations = Clone.DeepClone(wordUnderInvestigation.GetVisitedLocations());
                    WordUnderInvestigation wordToCheck = new WordUnderInvestigation(possibleWord, visitedLocations, _bruteForceSearchLimit);

                    Search(wordToCheck, visitedPoints, board, x, y, foundWords);
                }
            }
        }

        /// <summary>
        /// Searches for words.
        /// Given an initial set of letters of length bruteForceSearchLimit, it is able to use a sub dictionary to check neighbouring tiles
        /// and identify if it is possible for make a word.
        /// </summary>
        public void Search(WordUnderInvestigation possibleWord, List<Point> visitedPoints, char[,] board, int x, int y, List<WordUnderInvestigation> foundWords)
        {
            //Case 1- goes off grid
            int boardLength = board.GetLength(0);
            if (x >= boardLength || x < 0 || y >= boardLength || y < 0)
            {
                return;
            }

            //Case 2-Already visited
            bool hasBeenVisited = visitedPoints.Any(point => point.HasValue(x, y));
            if (hasBeenVisited) return;

            visitedPoints.Add(new Point(x, y));

            //Case 3- Success! No letters left in possible word
            if (possibleWord.IsCurrentSearchIndexAtEndOfWord())
            {
                foundWords.Add(possibleWord);
                return;
            }

            //Case 4- not the next char in this word
            if (board[x, y] != possibleWord.GetCharAtCurrentSearchIndex())
            {
                return;
            }

            possibleWord.AddVisitedLocationAtCurrentSearchPosition(new Point(x, y), board[x, y]); // NB: Must occur before increment index
            possibleWord.IncrementCurrentSearchIndex();
            SmartFindSurroundingWords(possibleWord, visitedPoints, board, x, y, foundWords);
        }

        private void SmartFindSurroundingWords(WordUnderInvestigation possibleWord, List<Point> visitedPoints, char[,] board, int x, int y, List<WordUnderInvestigation> foundWords)
        {
            List<Point> clonedVisitedPoints = new List<Point>(visitedPoints);
            WordUnderInvestigation clonedPossibleWord = Clone.DeepClone(possibleWord);
            Search(clonedPossibleWord, clonedVisitedPoints, board, x - 1, y - 1, foundWords);

            clonedPossibleWord = Clone.DeepClone(possibleWord);
            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedPossibleWord, clonedVisitedPoints, board, x, y - 1, foundWords);

            clonedPossibleWord = Clone.DeepClone(possibleWord);
            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedPossibleWord, clonedVisitedPoints, board, x + 1, y - 1, foundWords);

            clonedPossibleWord = Clone.DeepClone(possibleWord);
            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedPossibleWord, clonedVisitedPoints, board, x - 1, y, foundWords);

            clonedPossibleWord = Clone.DeepClone(possibleWord);
            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedPossibleWord, clonedVisitedPoints, board, x + 1, y, foundWords);

            clonedPossibleWord = Clone.DeepClone(possibleWord);
            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedPossibleWord, clonedVisitedPoints, board, x - 1, y + 1, foundWords);

            clonedPossibleWord = Clone.DeepClone(possibleWord);
            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedPossibleWord, clonedVisitedPoints, board, x, y + 1, foundWords);

            clonedPossibleWord = Clone.DeepClone(possibleWord);
            clonedVisitedPoints = new List<Point>(visitedPoints);
            Search(clonedPossibleWord, clonedVisitedPoints, board, x + 1, y + 1, foundWords);
        }
    }
}