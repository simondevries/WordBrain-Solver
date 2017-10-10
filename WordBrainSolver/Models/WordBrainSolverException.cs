using System;

namespace WordBrainSolver.Core.Models
{
    public class WordBrainSolverException : Exception
    {
        public WordBrainSolverException(string message) : base(message)
        {
        }
    }
}