using System;
using System.Collections.Generic;

namespace WordBrainSolver.Core.Models
{
    /// <summary>
    /// Represents a dictionary and a subdictionary
    /// </summary>
    public class WordDictionaries
    {
        private readonly string[] _fullDictionary;
        private readonly Dictionary<string, Dictionary<string, IEnumerable<string>>> _subDictionaries;

        /// <summary>
        /// Constructor
        /// </summary>
        public WordDictionaries(string[] fullDictionary)
        {
            _fullDictionary = fullDictionary;
            _subDictionaries = new Dictionary<string, Dictionary<string, IEnumerable<string>>>();
        }

        /// <summary>
        /// Gets the full word dictionary
        /// </summary>
        public string[] GetFullDictionary()
        {
            return _fullDictionary;
        }

        /// <summary>
        /// Returns whether the full dictionary is already generated
        /// </summary>
        /// <returns></returns>
        public bool IsFullDictionaryGenerated()
        {
            return _fullDictionary != null && _fullDictionary.Length > 0;
        }

        /// <summary>
        /// Sets the sub dictionary used in the intelligent secondary word search
        /// </summary>
        public void AddItemToSubDictionary(string key, Dictionary<string, IEnumerable<string>> subDictionary)
        {
            _subDictionaries.Add(key, subDictionary);
        }

        /// <summary>
        /// Returns a sub dictionary used in the intelligent secondary word search
        /// </summary>
        public Dictionary<string, IEnumerable<string>> GetSubDictionary(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("You have not specified a key");
            }

            if (_subDictionaries.ContainsKey(key))
            {
                return _subDictionaries[key];
            }

            throw new KeyNotFoundException($"There does not exist an entry for the key: {key} in the dictionary");
        }

        public bool DoesSubDictionaryHaveItem(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("You have not specified a key");
            }

            return _subDictionaries.ContainsKey(key);
        }
    }
}