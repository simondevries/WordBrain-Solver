using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.API.Controllers
{
    [AllowCrossSiteJson]
    public class FindWordsController : ApiController
    {
        private readonly IGameCoordinator _gameCoordinator;

        public FindWordsController(IGameCoordinator gameCoordinator)
        {
            _gameCoordinator = gameCoordinator ?? throw new ArgumentNullException(nameof(gameCoordinator));
        }

        // GET api/<controller>/5
        public string Get(int gridSize, int wordLength, string board)
        {
            List<string> list = _gameCoordinator.GenerateGameSolutions(wordLength, gridSize, board);

            string aggregate = list.Aggregate("", (c, s) => s + ", " + c);
            return aggregate;
        }
    }
}