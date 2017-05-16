using System;
using System.Collections.Generic;
using System.IO;

namespace WordBrainSolver.Tests
{
    public class TestResultsSaver
    {
        private const string DictionaryFileName = "testResults.csv";
        private const string ResourcesFolderName = "Resources";

        public void SaveResults(string time, TestCase testCase, List<string> results)
        {
            string resultsFormatted = string.Empty;
            foreach (string result in results)
            {
                resultsFormatted += ";" + result;
            }

            string text = string.Format("{0},{1},{2},{3},{4}, {5}, {6}", DateTime.Now, testCase.Board, testCase.GridSize, testCase.Lives, time, resultsFormatted, Environment.MachineName);

            string path = Path.Combine(Directory.GetCurrentDirectory(), $@"{ResourcesFolderName}\{DictionaryFileName}");

            if (!File.Exists(path))
            {
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Date, board, gridSize, lives, Time in MS, results, machine name");
                        sw.WriteLine(text);
                    }
                    return;
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(text);
            }
        }
    }
}