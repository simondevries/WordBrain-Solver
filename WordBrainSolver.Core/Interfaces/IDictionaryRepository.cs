using System.Threading.Tasks;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Interfaces
{
    /// <summary>
    /// Responsible for retrieving the dictionary
    /// </summary>
    public interface IDictionaryRepository
    {
        /// <summary>
        /// Retrieves the content of the dictionary.
        /// </summary>
        Task<WordDictionaries> RetrieveFullDictionary();
    }
}