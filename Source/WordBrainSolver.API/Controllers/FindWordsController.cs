using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

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
        public async Task<IActionResult> Post(FindWordsRequestDto findWordsRequestDto)
        {
            if (findWordsRequestDto.Board == null) throw new Exception("findWordsRequestDto is null");
            if (findWordsRequestDto.WordLength == null) throw new Exception("WordLength is null");

            List<string> list = await _solutionGeneratorCoordinator.GenerateGameSolutions(findWordsRequestDto.WordLength, findWordsRequestDto.Board);

            string aggregate = list.Aggregate("", (c, s) => s + "   ~ ~ ~   " + c);
            return Ok(aggregate);
        }
    }
}
