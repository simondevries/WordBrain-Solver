using System.Collections.Generic;

namespace WordBrainSolver.Core.Interfaces
{
    public interface ISubDictionaryGenerator
    {
        Dictionary<string, List<string>> GenerateSubDictionary(int keyLength, int wordLengthBeingSearchedFor, string[] dictionary);
    }
}