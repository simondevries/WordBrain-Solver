using System;
using System.Collections.Generic;

namespace WordBrainSolver.Core.Models
{
    [Serializable]
    public class WordUnderInvestigation
    {
        private string _word;
        private readonly List<Point> _visitedLocations;
        private int _currentSearchIndex;

        public WordUnderInvestigation()
        {
            _word = string.Empty;
            _visitedLocations = new List<Point>();
            _currentSearchIndex = 0;
        }

        public WordUnderInvestigation(string word, List<Point> visitedLocations, int currentSearchIndex)
        {
            _word = word;
            _visitedLocations = visitedLocations;
            _currentSearchIndex = currentSearchIndex;
        }

        /// <summary>
        /// CurrentSearchIndex is used by IntelligentSecondaryWordSearcher
        /// </summary>
        public void SetCurrentSearchIndex(int index)
        {
            _currentSearchIndex = index;
        }

        public bool IsCurrentSearchIndexAtEndOfWord()
        {
            return _currentSearchIndex >= _word.Length;
        }

        public void IncrementCurrentSearchIndex()
        {
            _currentSearchIndex++;
        }

        public char GetCharAtCurrentSearchIndex()
        {
            return _word[_currentSearchIndex];
        }

        public string GetWord()
        {
            return _word;
        }

        public List<Point> GetVisitedLocations()
        {
            return _visitedLocations;
        }

        /// <summary>
        /// Adds a visited location at the current search index. Checks that the expected char at that index is what is passed in
        /// </summary>
        public void AddVisitedLocationAtCurrentSearchPosition(Point point, char character)
        {
            if (_word[_currentSearchIndex] != character)
            {
                throw new Exception("The passed in character did not matched the character for that index");
            }
            if (_visitedLocations.Count == _currentSearchIndex - 1)
            {
                _visitedLocations.Add(point);
            }
            else if (_visitedLocations.Count >= _currentSearchIndex)
            {
                _visitedLocations[_currentSearchIndex - 1] = point;
            }
            else
            {
                throw new Exception("Visied Locations list is not initialized at that index");
            }
        }

        public void AddCharacter(char character, Point point)
        {
            _word += character;
            _visitedLocations.Add(point);
        }
        public void AddCharacter(char character, int x, int y)
        {
            _word += character;
            _visitedLocations.Add(new Point(x, y));
        }

        public bool HasLength(int len)
        {
            return _word.Length == len;
        }

        public void RemoveLastCharacter()
        {
            _word.Remove(_word.Length - 1);
            _visitedLocations.RemoveAt(_visitedLocations.Count - 1);
        }
    }
}