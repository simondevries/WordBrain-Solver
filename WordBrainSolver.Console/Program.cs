using System;
using Ninject;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new NinjectModule());
            IGameCoordinator gameCoordinator = kernel.Get<IGameCoordinator>();

            while (true)
            {
                System.Console.WriteLine("Enter Grid Size, for example '3' or '4'");
                int gridSize = Convert.ToInt32(System.Console.ReadLine());

                System.Console.WriteLine("Enter Board from left to right top to bottom");
                string board = System.Console.ReadLine();

                System.Console.WriteLine("Enter Word Length");
                int lives = Convert.ToInt32(System.Console.ReadLine());

                var generatedGameSolutions = gameCoordinator.GenerateGameSolutions(lives, gridSize, board);
                foreach (var solution in generatedGameSolutions)
                {
                    System.Console.WriteLine(solution);
                }

                System.Console.WriteLine("Done");
                System.Console.WriteLine("=============");
            }
        }
    }
}