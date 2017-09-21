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
            //todo For the expected results, I cannot confirm that the value entered is actually the number of solutions of a puzzle.
            List<TestCase> testCases = new List<TestCase>
            {
                new TestCase {Board = "webtrsaicnibstwr", Lives = new [] {5, 5, 6}, ExpectedResults = 2}, // Sheep Level 6
                new TestCase {Board = "lhmbaaoeimodrear", Lives = new [] {7, 3, 6}, ExpectedResults = 2},
                new TestCase {Board = "ysonelnnhncaolab", Lives = new [] {5, 5, 6}, ExpectedResults = 2} 
            };

            foreach (TestCase testCase in testCases)
            {
                RunTest(testCase);
            }
        }

        private void RunTest(TestCase testCase)
        {
            SolutionGeneratorCoordinator solverSolutionGeneratorCoordinatorCoordinator = new SolutionGeneratorCoordinatorBuilder().Build();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<string> results = solverSolutionGeneratorCoordinatorCoordinator.GenerateGameSolutions(testCase.Lives, testCase.Board);

            results.Count.Should().Be(2);

            stopwatch.Stop();

            _testResultsSaver = new TestResultsSaver();

            _testResultsSaver.SaveResults(stopwatch.ElapsedMilliseconds.ToString(), testCase, results);

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
//
//        [Test]
//        public void RunTest()
//        {
//            //1
//            TestCase testCase = new TestCase {Board = "abcdefghijklmnop", Lives = 3, GridSize = 4};
//
//            SolutionGeneratorCoordinator solverSolutionGeneratorCoordinatorCoordinator = new SolutionGeneratorCoordinatorBuilder().Build();
//
//            List<string> results = solverSolutionGeneratorCoordinatorCoordinator.GenerateGameSolutionsForGameState(testCase.Lives, testCase.GridSize, testCase.Board);
//
//            results.Should().NotBeNull();
//            results.Count.Should().Be(8);
//
//            //2
//
//             testCase = new TestCase { Board = "nshdakuiaoisskeu", Lives = 3, GridSize = 4 };
//            
//            results = solverSolutionGeneratorCoordinatorCoordinator.GenerateGameSolutionsForGameState(testCase.Lives, testCase.GridSize, testCase.Board);
//
//            results.Should().NotBeNull();
//            results.Count.Should().Be(20);
//        }
    }
}
