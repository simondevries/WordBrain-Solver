using System;

namespace WordBrainSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Game game = new Game();

                Console.WriteLine("Enter Grid Size, for example '3' or '4'");
                int gridSize = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Board from left to right top to bottom");
                string board = Console.ReadLine();

                Console.WriteLine("Enter Word Length");
                int lives = Convert.ToInt32(Console.ReadLine());

                game.RunGame(lives, gridSize, board);

                Console.WriteLine("Done");
                Console.WriteLine("=============");
            }
        }
    }
}
