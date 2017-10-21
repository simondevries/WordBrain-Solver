using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Tests.Builders;

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
                // Not yet supported new TestCase {Board = "ewcuopisclmeatkl", Lives = new [] {3, 3, 5, 5}, MinimumExpectedResults = 4},
                new TestCase {Board = "beomalpblaaotthr", Lives = new[] {5, 8, 3}, MinimumResultsExpected = 1},
                new TestCase {Board = "batmatcat", Lives = new[] {3,3,3}, MinimumResultsExpected = 1},
                new TestCase {Board = "kekmonlrbsuldahcdaoekperd", Lives = new[] {5, 6, 7, 7}, MinimumResultsExpected = 1},
                new TestCase {Board = "esngeeeihpyrctok", Lives = new[] {6, 3, 7}, MinimumResultsExpected = 1},
                new TestCase {Board = "btenitelknzzarup", Lives = new[] {6, 6, 4}, MinimumResultsExpected = 1},
                new TestCase {Board = "kltnccaolooobvob", Lives = new[] {5, 7, 4}, MinimumResultsExpected = 1},
                new TestCase {Board = "bruoulerltnnleoi", Lives = new[] {5, 6, 5}, MinimumResultsExpected = 1},
                new TestCase {Board = "tdttoeiconnebims", Lives = new[] {4, 5, 7}, MinimumResultsExpected = 1},
                new TestCase {Board = "lhmbaaoeimodrear", Lives = new [] {7, 3, 6}, MinimumResultsExpected = 1},
                new TestCase {Board = "ysonelnnhncaolab", Lives = new [] {5, 5, 6}, MinimumResultsExpected = 1}
            };

            foreach (TestCase testCase in testCases)
            {
                RunTest(testCase);
            }
        }

        private void RunTest(TestCase testCase)
        {
            SolutionGeneratorCoordinator solverSolutionGeneratorCoordinatorCoordinator =
                new SolutionGeneratorCoordinatorBuilder().Build();


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<string> results = solverSolutionGeneratorCoordinatorCoordinator.GenerateGameSolutions(testCase.Lives,
                testCase.Board);

            results.Count.Should().BeGreaterOrEqualTo(testCase.MinimumResultsExpected);


            _testResultsSaver = new TestResultsSaver();

            stopwatch.Stop();

            _testResultsSaver.SaveResults(stopwatch.ElapsedMilliseconds.ToString(), testCase, results);

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
