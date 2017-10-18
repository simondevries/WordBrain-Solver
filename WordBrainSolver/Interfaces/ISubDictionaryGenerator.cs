using System.Collections.Generic;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Responsible for generating the sub dictionary
    /// </summary>
    public interface ISubDictionaryGenerator
    {
        /// <summary>
        /// Adds a sub dictionary to the word dictionary for the specified word length being searched for
        /// </summary>
        Dictionary<string, IEnumerable<string>> RetrieveSubDictionaryForWord(int wordLengthBeingSearchedFor,
            WordDictionaries wordDictionaries);
    }
}