using System.Collections.Generic;

namespace WordBrainSolver
{
    public class SubDictionaryGenerator
    {
        public Dictionary<string, List<string>> GenerateSubDictionary(int wordLengthBeingSearchedFor, string[] dictionary, int keyLength)
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
                        subDictionary.Add(keyLetters, new List<string>() { word });
                    }

                }
            }

            return subDictionary;
        }
    }
}