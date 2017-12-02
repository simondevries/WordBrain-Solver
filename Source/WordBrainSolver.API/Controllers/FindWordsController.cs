using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordBrainSolver.Api.Models;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Api.Controllers
{
    [Route("api/[controller]")]
    public class FindWordsController : Controller
    {
        private readonly ISolutionGeneratorCoordinator _solutionGeneratorCoordinator;

        public FindWordsController(ISolutionGeneratorCoordinator solutionGeneratorCoordinator)
        {
            _solutionGeneratorCoordinator = solutionGeneratorCoordinator ?? throw new ArgumentNullException(nameof(solutionGeneratorCoordinator));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FindWordsRequestDto findWordsRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<string> list = await _solutionGeneratorCoordinator.GenerateGameSolutions(findWordsRequestDto.WordLength, findWordsRequestDto.Board);

            string aggregate = list.Aggregate("", (c, s) => s + Environment.NewLine + c);
            return Ok(aggregate);
        }
    }
}
