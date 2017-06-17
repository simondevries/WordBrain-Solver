﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.API.Controllers
{
    [AllowCrossSiteJson]
    public class FindWordsController : ApiController
    {
        private readonly ISolutionGeneratorCoordinator _solutionGeneratorCoordinator;

        public FindWordsController(ISolutionGeneratorCoordinator solutionGeneratorCoordinator)
        {
            _solutionGeneratorCoordinator = solutionGeneratorCoordinator;
                // ?? throw new ArgumentNullException(nameof(solutionGeneratorCoordinator));
        }

        // GET api/<controller>/5
        public string Get(int gridSize, int wordLength, string board)
        {
            List<string> list = _solutionGeneratorCoordinator.GenerateGameSolutions(wordLength, gridSize, board);

            string aggregate = list.Aggregate("", (c, s) => s + ", " + c);
            return aggregate;
        }
    }
}