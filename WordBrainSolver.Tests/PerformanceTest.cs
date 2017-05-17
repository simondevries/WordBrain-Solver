using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordBrainSolver.Core;

namespace WordBrainSolver.Tests
{
    [TestClass]
    public class PerformanceTest
    {

        private TestResultsSaver _testResultsSaver;

        public PerformanceTest()
        {
            _testResultsSaver = new TestResultsSaver();
        }

        [TestMethod]
        public void PerformanceTestOne()
        {
            List<TestCase> testCases = new List<TestCase>
            {
                new TestCase {Board = "btleexffoiretahs", Lives = 5, GridSize = 4},
                new TestCase {Board = "btleexffoiretahs", Lives = 4, GridSize = 4},
                new TestCase {Board = "reocutbrcwrmipoo", Lives = 7, GridSize = 4},
                new TestCase {Board = "reocutbrcwrmipoo", Lives = 3, GridSize = 4},
                new TestCase {Board = "cphtealscueoshog", Lives = 8, GridSize = 4},
                new TestCase {Board = "ullkkssoc", Lives = 5, GridSize = 3},
                new TestCase {Board = "ullkkssoc", Lives = 3, GridSize = 3},
                new TestCase {Board = "ullkkssoc", Lives = 4, GridSize = 3},
            };

            foreach (TestCase testCase in testCases)
            {
                RunTest(testCase);
            }
        }

        private void RunTest(TestCase testCase)
        {
            GameCoordinator gameCoordinator = new GameCoordinator();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<string> results = gameCoordinator.GenerateGameSolutions(testCase.Lives, testCase.GridSize, testCase.Board);

            stopwatch.Stop();

            _testResultsSaver = new TestResultsSaver();

            _testResultsSaver.SaveResults(stopwatch.ElapsedMilliseconds.ToString(), testCase, results);

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
