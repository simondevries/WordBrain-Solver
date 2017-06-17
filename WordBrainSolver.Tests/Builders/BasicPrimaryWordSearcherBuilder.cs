﻿using WordBrainSolver.Core.Algorithm;

namespace WordBrainSolver.Tests.Builders
{
    public class BasicPrimaryWordSearcherBuilder
    {
        public BasicPrimaryWordSearcher Build()
        {
            return new BasicPrimaryWordSearcher(new IntelligentSecondaryWordSearcher(), Settings.BruteForceSearchLimit);
        }
    }
}