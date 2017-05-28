using System;
using System.IO;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Core
{
    public class FileDictionaryRetriever : IDictionaryRetriever
    {
        private readonly string _dictionaryPath;

        public FileDictionaryRetriever(string dictionaryPath)
        {
            _dictionaryPath = dictionaryPath ?? throw new ArgumentNullException(nameof(dictionaryPath));
        }

        public string[] RetrieveDictionaryContent()
        {
            if (!File.Exists(_dictionaryPath))
                throw new FileNotFoundException($"Dictionary file was not found at path: '{_dictionaryPath}'");

            using (StreamReader streamReader = new StreamReader(_dictionaryPath))
            {
                string content = streamReader.ReadToEnd();
                return content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}