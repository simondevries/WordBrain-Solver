using System.Collections.Generic;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Core
{
    public class SubDictionaryGenerator : ISubDictionaryGenerator
    {
        public Dictionary<string, List<string>> GenerateSubDictionary(int keyLength, int wordLengthBeingSearchedFor, string[] dictionary)
        {
            Dictionary<string, List<string>> subDictionary = new Dictionary<string, List<string>>();

            foreach (string word in dictionary)
            {
                if (word.Length == wordLengthBeingSearchedFor)
                {
                    string keyLetters = word.Substring(0, keyLength);
                    if (subDictionary.ContainsKey(keyLetters))
                    {
                        subDictionary[keyLetters].Add(word);
                    }
                    else
                    {
                        subDictionary.Add(keyLetters, new List<string> { word });
                    }

                }
            }

            return subDictionary;
        }
    }
}