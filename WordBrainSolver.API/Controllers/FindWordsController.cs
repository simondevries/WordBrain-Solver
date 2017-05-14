using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace WordBrainSolver.API.Controllers
{
    [AllowCrossSiteJson]
    public class FindWordsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/<controller>/5
        public string Get(int gridSize, int wordLength, string board)
        {

            Game game = new Game();
            List<string> list = game.RunGame(wordLength, gridSize, board);

            string aggregate = list.Aggregate("", (c, s) => s + ", " + c);
            return aggregate;
        }
    }
}