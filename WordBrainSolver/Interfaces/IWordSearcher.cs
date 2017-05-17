using System.Collections.Generic;

namespace WordBrainSolver.Core.Interfaces
{
    public interface IWordSearcher
    {
        List<string> FindWords(List<Point> visitedPoints, int lives, int x, int y, string currentWord, char[,] board);
    }
}