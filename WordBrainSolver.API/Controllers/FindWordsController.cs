using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.API.Controllers
{
    [RequireHttps]
    public class FindWordsController : ApiController
    {
        private readonly ISolutionGeneratorCoordinator _solutionGeneratorCoordinator;

        public FindWordsController(ISolutionGeneratorCoordinator solutionGeneratorCoordinator)
        {
            _solutionGeneratorCoordinator = solutionGeneratorCoordinator;
                // ?? throw new ArgumentNullException(nameof(solutionGeneratorCoordinator));
        }

        [System.Web.Http.HttpGet]
        public string Get()
        {
            return "test";
        }

        // GET api/<controller>/5
        [System.Web.Http.HttpPost]
        public string Post(FindWordsRequestDto findWordsRequestDto)
        {
            if (findWordsRequestDto.Board == null) throw new Exception("findWordsRequestDto is null");
            if (findWordsRequestDto.WordLength == null) throw new Exception("WordLength is null");

            List<string> list = _solutionGeneratorCoordinator.GenerateGameSolutions(findWordsRequestDto.WordLength, findWordsRequestDto.Board);

            string aggregate = list.Aggregate("", (c, s) => s + " & " + c);
            return aggregate;
        }
    }
}