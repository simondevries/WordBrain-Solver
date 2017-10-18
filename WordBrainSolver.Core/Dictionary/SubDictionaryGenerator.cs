using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Dictionary
{
    /// <summary>
    /// Responsible for generating the sub dictionary
    /// </summary>
    public class SubDictionaryGenerator : ISubDictionaryGenerator
    {
        private readonly int _bruteForceSearchLimit;

        /// <summary>
        /// Constructor
        /// </summary>
        public SubDictionaryGenerator(int bruteForceSearchLimit)
        {
            _bruteForceSearchLimit = bruteForceSearchLimit;
        }

        /// <summary>
        /// Returns a sub dictionary for the specified word length being searched for
        /// Adds the entry to the current wordDictionaries for future access.
        /// </summary>
        public Dictionary<string, IEnumerable<string>> RetrieveSubDictionaryForWord(int wordLengthBeingSearchedFor,
            WordDictionaries wordDictionaries)
        {
            // TODO Make sure this is thread-safe.
            if (!wordDictionaries.IsFullDictionaryGenerated())
            {
                throw new InvalidOperationException(
                    "Cannot generatre sub dictionary when full dictionary is empty or not initialized");
            }

            int _bruteForceSearchLimit = 3;
            var key = $"{_bruteForceSearchLimit},{wordLengthBeingSearchedFor}";
            if (wordDictionaries.DoesSubDictionaryHaveItem(key))
            {
                return wordDictionaries.GetSubDictionary(key);
            }

            Dictionary<string, IEnumerable<string>> generatedSubDictionary =
                GenerateSubDictionaryFromFullDictionary(wordLengthBeingSearchedFor, wordDictionaries);
            wordDictionaries.AddItemToSubDictionary(key, generatedSubDictionary);
            return generatedSubDictionary;
        }

        private Dictionary<string, IEnumerable<string>> GenerateSubDictionaryFromFullDictionary(
            int wordLengthBeingSearchedFor, WordDictionaries wordDictionaries)
        {
            Dictionary<string, IEnumerable<string>> subDictionary = new Dictionary<string, IEnumerable<string>>();
            foreach (string word in wordDictionaries.GetFullDictionary())
            {
                if (word.Length == wordLengthBeingSearchedFor)
                {
                    string keyLetters = word.Substring(0, _bruteForceSearchLimit);
                    if (subDictionary.ContainsKey(keyLetters))
                    {
                        IEnumerable<string> enumerable = subDictionary[keyLetters];
                        enumerable = enumerable.Concat(new[] {word});
                        subDictionary[keyLetters] = enumerable;
                    }
                    else
                    {
                        subDictionary.Add(keyLetters, new List<string> {word});
                    }
                }
            }
            return subDictionary;
        }
    }
}
