using System;
using System.Collections.Generic;

namespace WordBrainSolver.Core.Models
{
    [Serializable]
    public class WordUnderInvestigation
    {
        private string _word;
        private readonly List<Point> _positionOfLettersInWord;
        private int _currentSearchIndex;

        public WordUnderInvestigation()
        {
            _word = string.Empty;
            _positionOfLettersInWord = new List<Point>();
            _currentSearchIndex = 0;
        }

        public WordUnderInvestigation(string word, List<Point> positionOfLettersInWord, int currentSearchIndex)
        {
            _word = word;
            _positionOfLettersInWord = positionOfLettersInWord;
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
            return _currentSearchIndex >= _word.Length - 1;
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
            return _positionOfLettersInWord;
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
            if (_positionOfLettersInWord.Count == _currentSearchIndex)
            {
                _positionOfLettersInWord.Add(point);
            }
            else if (_positionOfLettersInWord.Count >= _currentSearchIndex)
            {
                _positionOfLettersInWord[_currentSearchIndex] = point;
            }
            else
            {
                throw new Exception("Visied Locations list is not initialized at that index");
            }
        }

        public void AddCharacter(char character, Point point)
        {
            _word += character;
            _positionOfLettersInWord.Add(point);
        }
        public void AddCharacter(char character, int x, int y)
        {
            _word += character;
            _positionOfLettersInWord.Add(new Point(x, y));
        }

        public bool HasLength(int len)
        {
            return _word.Length == len;
        }

        public void RemoveLastCharacter()
        {
            _word.Remove(_word.Length - 1);
            _positionOfLettersInWord.RemoveAt(_positionOfLettersInWord.Count - 1);
        }
    }
}