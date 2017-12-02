using System;
using System.Collections.Generic;

namespace WordBrainSolver.Core.Algorithm
{
    public class OrderOfExecutionInitializer
    {
        private SolutionGeneratorCoordinator _solutionGeneratorCoordinator;

        public OrderOfExecutionInitializer(SolutionGeneratorCoordinator solutionGeneratorCoordinator)
        {
            _solutionGeneratorCoordinator = solutionGeneratorCoordinator;
        }

        public List<List<int>> InitializeOrderOfExecution(int wordLengthsLength)
        {
            switch (wordLengthsLength)
            {
                case (3):
                    return new List<List<int>> {
                        new List<int> { 0, 1, 2},
                        new List<int> { 0, 2, 1},
                        new List<int> { 1, 0, 2},
                        new List<int> { 1, 2, 0},
                        new List<int> { 2, 0, 1},
                        new List<int> { 2, 1, 0}
                    };
                case (4):
                    return new List<List<int>> {
                        new List<int> { 0, 1, 2, 3},
                        new List<int> { 0, 1, 3, 2},
                        new List<int> { 0, 2, 1, 3},
                        new List<int> { 0, 2, 3, 1},
                        new List<int> { 0, 3, 2, 1},
                        new List<int> { 0, 3, 1, 2},

                        new List<int> { 1, 0, 2, 3},
                        new List<int> { 1, 0, 3, 2},
                        new List<int> { 1, 2, 0, 3},
                        new List<int> { 1, 2, 3, 0},
                        new List<int> { 1, 3, 2, 0},
                        new List<int> { 1, 3, 0, 2},

                        new List<int> { 2, 1, 0, 3},
                        new List<int> { 2, 1, 3, 0},
                        new List<int> { 2, 0, 1, 3},
                        new List<int> { 2, 0, 3, 1},
                        new List<int> { 2, 3, 0, 1},
                        new List<int> { 2, 3, 1, 0},

                        new List<int> { 3, 1, 2, 0},
                        new List<int> { 3, 1, 0, 2},
                        new List<int> { 3, 2, 1, 0},
                        new List<int> { 3, 2, 0, 1},
                        new List<int> { 3, 0, 2, 1},
                        new List<int> { 3, 0, 1, 2}
                    };
                default:
                    throw new Exception("Could not initialize oreder of execution");
            }
        }
    }
}