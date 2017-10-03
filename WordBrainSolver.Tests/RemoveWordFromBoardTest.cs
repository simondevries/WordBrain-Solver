using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Tests
{
    [TestFixture]
    public class RemoveWordFromBoardTest
    {
        private IRemoveWordFromBoard _removeWordFromBoard;

        [SetUp]
        public void SetUp()
        {
            _removeWordFromBoard = new RemoveWordFromBoard();
        }

        [Test]
        public void CanSuccessfullyRemoveWordsFromBoard()
        {
            //  Screw -> Twins -> Rabbit
            char[,] board = _inputBoard;
            List<Point> pointsToRemove = new List<Point>()
            {
                new Point(0,0),
                new Point(0,1),
                new Point(1,0),
                new Point(2,0),
                new Point(3,0)
            };

            board = _removeWordFromBoard.RemoveWords(board, pointsToRemove, 4);
            
            board.Should().Equal(_expectedBoardAfterIterationOne);

            pointsToRemove = new List<Point>()
            {
                new Point(3,1),
                new Point(3,2),
                new Point(2,1),
                new Point(2,2),
                new Point(1,1),
            };

            board = _removeWordFromBoard.RemoveWords(board, pointsToRemove, 4);
            
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