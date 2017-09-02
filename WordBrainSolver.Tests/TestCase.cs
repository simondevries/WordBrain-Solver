using System.Collections.Generic;

namespace WordBrainSolver.Tests
{
    public class TestCase
    {
        public List<int> Lives { get; set; }
        public string Board { get; set; }
        public int GridSize { get; set; }
        public int ExpectedResults { get; set; }
    }
}