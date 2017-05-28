using System.Collections.Generic;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Responsible for managing words dictionary and it's sub dictionaries.
    /// </summary>
    public interface IDictionaryCoordinator
    {
        /// <summary>
        /// Retrieves the content of the dictionary.
        /// </summary>
        void RetrieveDictionaryContent();
        /// <summary>
        /// Generates the sub dictionary.
        /// </summary>
        Dictionary<string, List<string>> GenerateSubDictionary(int brutForceSearchLimit, int wordLengthBeingSearchedFor);
    }
}