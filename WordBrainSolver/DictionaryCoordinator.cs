using System;
using System.Collections.Generic;
using System.Linq;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Core
{
    /// <summary>
    /// Responsible for managing words dictionary and it's sub dictionaries. This class should be injected as a singleton.
    /// </summary>
    public class DictionaryCoordinator : IDictionaryCoordinator
    {
        string[] _retrieveDictionaryContent;
        private readonly IDictionaryRetriever _dictionaryRetriever;
        private readonly ISubDictionaryGenerator _subDictionaryGenerator;
        private readonly Dictionary<string, Dictionary<string, List<string>>> _allSubDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryCoordinator"/> class.
        /// </summary>
        public DictionaryCoordinator(ISubDictionaryGenerator subDictionaryGenerator, IDictionaryRetriever dictionaryRetriever)
        {
            _subDictionaryGenerator = subDictionaryGenerator ?? throw new ArgumentNullException(nameof(subDictionaryGenerator));
            _dictionaryRetriever = dictionaryRetriever ?? throw new ArgumentNullException(nameof(dictionaryRetriever));
            _allSubDictionary = new Dictionary<string, Dictionary<string, List<string>>>();
        }

        /// <summary>
        /// Retrieves the content of the dictionary.
        /// </summary>
        public void RetrieveDictionaryContent()
        {
            // TODO Implement caching.
            // TODO Make sure this is thread-safe.
            _retrieveDictionaryContent = _dictionaryRetriever.RetrieveDictionaryContent();
            _allSubDictionary.Clear();
        }

        /// <summary>
        /// Generates the sub dictionary.
        /// </summary>
        public Dictionary<string, List<string>> GenerateSubDictionary(int brutForceSearchLimit, int wordLengthBeingSearchedFor)
        {
            if (_retrieveDictionaryContent == null)
            {
                RetrieveDictionaryContent();
            }

            // TODO Make sure this is thread-safe.
            var key = $"{brutForceSearchLimit},{wordLengthBeingSearchedFor}";
            if (!_allSubDictionary.Keys.Contains(key))
            {
                _allSubDictionary[key] = _subDictionaryGenerator.GenerateSubDictionary(brutForceSearchLimit, wordLengthBeingSearchedFor, _retrieveDictionaryContent);
            }
            return _allSubDictionary[key];
        }
    }
}