using System;
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
        private readonly int _bruteForceSearchLimit;
        private static readonly IEnumerable<Point> NeighbouringPoints = new List<Point>
        {
            new Point(- 1, - 1),
            new Point(0, - 1),
            new Point(+ 1, - 1),
            new Point(- 1, 0),
            new Point(+ 1, 0),
            new Point(- 1, + 1),
            new Point(0, + 1),
            new Point(+ 1, + 1),
        };

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
        public void InitiateSearch(List<Point> visitedPoints, int x, int y,
            WordUnderInvestigation wordUnderInvestigation, char[,] board,
            List<WordUnderInvestigation> foundWords, Dictionary<string, IEnumerable<string>> subDictionary)
        {
            if (!wordUnderInvestigation.HasLength(_bruteForceSearchLimit))
            {
                return;
            }

            if (subDictionary.ContainsKey(wordUnderInvestigation.GetWord()))
            {
                int lengthOfWordBeingSearched = subDictionary[wordUnderInvestigation.GetWord()].First().Length;
                Search(subDictionary[wordUnderInvestigation.GetWord()].ToList(), visitedPoints, board, x, y, foundWords,
                    _bruteForceSearchLimit - 1, lengthOfWordBeingSearched); // todo (sdv) can I bump brute fore search limut up
            }
        }

        /// <summary>
        /// Searches for words.
        /// Given an initial set of letters of length bruteForceSearchLimit, it is able to use a sub dictionary to check neighbouring tiles
        /// and identify if it is possible for make a word.
        /// </summary>
        public void Search(IEnumerable<string> possibleWords, List<Point> visitedPoints, char[,] board, int x, int y, List<WordUnderInvestigation> foundWords, int currentIndex, int lengthOfWordBeingSearched)
        {
            // Base Case 1- Already visited
            bool hasBeenVisited = visitedPoints.Any(point => point.HasValue(x, y));
            if (hasBeenVisited) return;


            // Base Case 2- word found
            if (currentIndex == possibleWords.First().Count() - 1)
            {
                foreach (string word in possibleWords)
                {
                    visitedPoints.Add(new Point(x, y));
                    WordUnderInvestigation foundWord = new WordUnderInvestigation(word, visitedPoints.ToArray(), 0); //current search index doesn't matter here cuz we found the word
                    foundWords.Add(foundWord);
                    visitedPoints.RemoveAt(visitedPoints.Count - 1);
                }
                return;
            }


            // Find neighbours that are compadible with the next char in possible words
            Dictionary<Point, List<string>> placesToCheck = new Dictionary<Point, List<string>>();
            int boardSize = board.GetLength(0) - 1;
            foreach (Point neighbouringPoint in NeighbouringPoints)
            {
                Point transformedPoint = new Point(x + neighbouringPoint.X(), y + neighbouringPoint.Y());
                if (transformedPoint.X() < 0 || transformedPoint.X() > boardSize || transformedPoint.Y() < 0 || transformedPoint.Y() > boardSize) continue;

                List<string> results = possibleWords.Where(word => board[transformedPoint.X(), transformedPoint.Y()] == word[currentIndex + 1]).ToList();
                if (results.Any())
                {
                    placesToCheck[transformedPoint] = results;
                }
            }


            visitedPoints.Add(new Point(x, y));
            currentIndex++;
            foreach (KeyValuePair<Point, List<string>> placeToCheck in placesToCheck)
            {
                Search(placeToCheck.Value, visitedPoints, board, placeToCheck.Key.X(), placeToCheck.Key.Y(), foundWords, currentIndex, lengthOfWordBeingSearched);
            }
            visitedPoints.RemoveAt(visitedPoints.Count - 1);
        }

    }
}