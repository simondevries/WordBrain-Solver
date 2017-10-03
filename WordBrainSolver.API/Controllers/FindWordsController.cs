using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

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

        [HttpGet]
        public string Get()
        {
            return "test";
        }

        // GET api/<controller>/5
        [HttpPost]
        public string Post(FindWordsRequestDto findWordsRequestDto)
        {
            List<string> list = _solutionGeneratorCoordinator.GenerateGameSolutions(findWordsRequestDto.WordLength, findWordsRequestDto.Board);

            string aggregate = list.Aggregate("", (c, s) => s + ", " + c);
            return aggregate;
        }
    }
}