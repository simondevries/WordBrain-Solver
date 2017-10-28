using WordBrainSolver.Core.Algorithm;

namespace WordBrainSolver.Core.Tests.Builders
{
    public class BasicPrimaryWordSearcherBuilder
    {
        public BasicPrimaryWordSearcher Build()
        {
            return new BasicPrimaryWordSearcher(new IntelligentSecondaryWordSearcher(Settings.BruteForceSearchLimit), Settings.BruteForceSearchLimit);
        }
    }
}