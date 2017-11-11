using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrainSolver.Core.Models
{
    public class WordUnderInvestigation
    {
        private string _word;
        private Point[] _positionOfLettersInWord;
        private int _currentSearchIndex;
        

        public WordUnderInvestigation()
        {
            _word = string.Empty;
            _positionOfLettersInWord = new Point[] {};
            _currentSearchIndex = 0;
        }

        public WordUnderInvestigation(string word, Point[] positionOfLettersInWord, int currentSearchIndex)
        {
            _word = word;
            _positionOfLettersInWord = positionOfLettersInWord;
            _currentSearchIndex = currentSearchIndex;
        }
        public WordUnderInvestigation(string word, List<Point> positionOfLettersInWord, int currentSearchIndex)
        {
            _word = word;
            _positionOfLettersInWord = positionOfLettersInWord.ToArray(); // todo(sdv) can I removie this toArray?
            _currentSearchIndex = currentSearchIndex;
        }
        public WordUnderInvestigation(WordUnderInvestigation wordUnderInvestigation)
        {
            _word = wordUnderInvestigation.GetWord();
            _positionOfLettersInWord = wordUnderInvestigation.GetVisitedLocations().ToArray(); // todo(sdv) can I removie this toArray?
            _currentSearchIndex = wordUnderInvestigation.GetCurrentSearchIndex();
        }

        /// <summary>
        /// CurrentSearchIndex is used by IntelligentSecondaryWordSearcher
        /// </summary>
        public void SetCurrentSearchIndex(int index)
        {
            _currentSearchIndex = index;
        }

        public int GetCurrentSearchIndex()
        {
            return _currentSearchIndex;
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

        public Point[] GetVisitedLocations()
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
            if (_positionOfLettersInWord.Length == _currentSearchIndex)
            {
                _positionOfLettersInWord = _positionOfLettersInWord.Concat(new []{point}).ToArray();
            }
            else if (_positionOfLettersInWord.Length >= _currentSearchIndex)
            {
                _positionOfLettersInWord[_currentSearchIndex] = point;
            }
            else
            {
                throw new Exception("Visied Locations list is not initialized at that index");
            }
        }

        /// <summary>
        /// Adds a visited location at the current search index. Checks that the expected char at that index is what is passed in
        /// </summary>
        public void SetVisitedPoints(Point[] points)
        {
            _positionOfLettersInWord = points;
        }


        public void AddCharacter(char character, Point point)
        {
            _word += character;
            _positionOfLettersInWord = _positionOfLettersInWord.Concat(new[] { point }).ToArray();
        }
        public void AddCharacter(char character, int x, int y)
        {
            _word += character;
            _positionOfLettersInWord = _positionOfLettersInWord.Concat(new[] { new Point( x, y) }).ToArray();
        }

        public bool HasLength(int len)
        {
            return _word.Length == len;
        }

        public void RemoveLastCharacter()
        {
            _word.Remove(_word.Length - 1);
            _positionOfLettersInWord =  _positionOfLettersInWord.Take(_positionOfLettersInWord.Count() - 1).ToArray();
        }
    }
}