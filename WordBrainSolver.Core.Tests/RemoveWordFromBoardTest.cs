using FluentAssertions;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Core.Models;
using Xunit;

namespace WordBrainSolver.Core.Tests
{
    public class RemoveWordFromBoardTest
    {
        [Fact]
        public void CanSuccessfullyRemoveWordsFromBoard()
        {
           var removeWordFromBoard = new RemoveWordFromBoard();

            //  Screw -> Twins -> Rabbit
            char[,] board = _inputBoard;
           Point[] pointsToRemove = new Point[]
            {
                new Point(0,0),
                new Point(0,1),
                new Point(1,0),
                new Point(2,0),
                new Point(3,0)
            };

            board = removeWordFromBoard.RemoveWords(board, pointsToRemove, 4);
            
            board.Should().Equal(_expectedBoardAfterIterationOne);

            pointsToRemove = new Point[]
            {
                new Point(3,1),
                new Point(3,2),
                new Point(2,1),
                new Point(2,2),
                new Point(1,1),
            };

            board = removeWordFromBoard.RemoveWords(board, pointsToRemove, 4);
            
            board.Should().Equal(_expectedBoardAfterIterationTwo);
        }


        /// <summary>
        /// Resources
        /// </summary>
        private readonly char[,] _inputBoard = {
                {'w','e','b','t'},
                {'r','s','a','i'},
                {'c','n','i','b'},
                {'s','t','w','r'}
        };
        private readonly char[,] _expectedBoardAfterIterationOne = {
                {'*','*','b','t'},
                {'*','s','a','i'},
                {'*','n','i','b'},
                {'*','t','w','r'}
        };
        private readonly char[,] _expectedBoardAfterIterationTwo = {
                {'*','*','*','t'},
                {'*','*','*','i'},
                {'*','*','b','b'},
                {'*','*','a','r'}
        };
    }
}