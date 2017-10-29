using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using FluentAssertions;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Core.Tests.Builders;
using Xunit;

namespace WordBrainSolver.Core.Tests
{
    public class PerformanceTest
    {

        private TestResultsSaver _testResultsSaver;

        public PerformanceTest()
        {
            _testResultsSaver = new TestResultsSaver();
        }

        [Fact]
        public async Task PerformanceTestOne()
        {
            //todo For the expected results, I cannot confirm that the value entered is actually the number of solutions of a puzzle.
            List<TestCase> testCases = new List<TestCase>
            {
                new TestCase {Board = "webtrsaicnibstwr", Lives = new[] {5, 5, 6}, ExpectedResults = 3},
                // Sheep Level 6
                new TestCase {Board = "lhmbaaoeimodrear", Lives = new [] {7, 3, 6}, ExpectedResults = 4},
                new TestCase {Board = "ysonelnnhncaolab", Lives = new [] {5, 5, 6}, ExpectedResults = 1}
            };

            foreach (TestCase testCase in testCases)
            {
                await RunTest(testCase);
            }
        }

        private async Task RunTest(TestCase testCase)
        {
            SolutionGeneratorCoordinator solverSolutionGeneratorCoordinatorCoordinator =
                new SolutionGeneratorCoordinatorBuilder().Build();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<string> results = await solverSolutionGeneratorCoordinatorCoordinator.GenerateGameSolutions(testCase.Lives,
                testCase.Board);

            results.Count.Should().Be(testCase.ExpectedResults);

            stopwatch.Stop();

            _testResultsSaver = new TestResultsSaver();

            _testResultsSaver.SaveResults(stopwatch.ElapsedMilliseconds.ToString(), testCase, results);

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        /*  [Fact]
            public void bar()
            {
                foo(string.Empty, 0, new[] { 0 }, new Box() { Size = 0 });
            }

            public void foo(string hello, int helloNumber, int[] helloArray, Box box)
            {
                if (helloNumber == 10) return;
                box.Size++;
                foo(hello + ".", helloNumber + 1, helloArray.Concat(new[] { 2 }).ToArray(), box);
            }

            public class Box
            {
                public int Size { get; set; }
            }
        */
    }
}
