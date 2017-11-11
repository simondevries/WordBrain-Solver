using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using FluentAssertions;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Core.Tests;
using WordBrainSolver.Core.Tests.Builders;
using Xunit;

namespace WordBrainSolver.Tests
{
    public class PerformanceTest
    {

        private TestResultsSaver _testResultsSaver;

        public PerformanceTest()
        {
            _testResultsSaver = new TestResultsSaver();
        }

        [Fact]
        public async void PerformanceTestOne()
        {
            List<TestCase> testCases = new List<TestCase>
            {
                new TestCase {Board = "ysonelnnhncaolab", Lives = new [] {5, 5, 6}},
                new TestCase {Board = "bruoulerltnnleoi", Lives = new[] {5, 6, 5}},
                new TestCase {Board = "beomalpblaaotthr", Lives = new[] {5, 8, 3}},
                new TestCase {Board = "esngeeeihpyrctok", Lives = new[] {6, 3, 7}},
                new TestCase {Board = "btenitelknzzarup", Lives = new[] {6, 6, 4}},
                new TestCase {Board = "kltnccaolooobvob", Lives = new[] {5, 7, 4}},
                new TestCase {Board = "tdttoeiconnebims", Lives = new[] {4, 5, 7}},
                new TestCase {Board = "lhmbaaoeimodrear", Lives = new [] {7, 3, 6}},
                new TestCase {Board = "batmatcat", Lives = new[] {3,3,3}},
                new TestCase {Board = "ewcuopisclmeatkl", Lives = new [] {3, 3, 5, 5}},
                new TestCase {Board = "kekmonlrbsuldahcdaoekperd", Lives = new[] {5, 6, 7, 7}}
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

            results.Count.Should().BeGreaterOrEqualTo(1);


            _testResultsSaver = new TestResultsSaver();

            stopwatch.Stop();

            _testResultsSaver.SaveResults(stopwatch.ElapsedMilliseconds.ToString(), testCase, results);

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}