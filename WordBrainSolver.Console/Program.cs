using System;
using WordBrainSolver.Core;

namespace WordBrainSolver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GameCoordinator gameCoordinator = new GameCoordinator();

                System.Console.WriteLine("Enter Grid Size, for example '3' or '4'");
                int gridSize = Convert.ToInt32(System.Console.ReadLine());

                System.Console.WriteLine("Enter Board from left to right top to bottom");
                string board = System.Console.ReadLine();

                System.Console.WriteLine("Enter Word Length");
                int lives = Convert.ToInt32(System.Console.ReadLine());

                gameCoordinator.GenerateGameSolutions(lives, gridSize, board);

                System.Console.WriteLine("Done");
                System.Console.WriteLine("=============");
            }
        }
    }
}
