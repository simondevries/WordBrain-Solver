//using System.Collections.Generic;
//using FluentAssertions;
//using NUnit.Framework;
//using WordBrainSolver.Core.Algorithm;
//using WordBrainSolver.Core.Models;
//using WordBrainSolver.Tests.Builders;
//
//namespace WordBrainSolver.Tests
//{
//    public class BasicPrimaryWordSearcherTest
//    {
//        readonly char[,] _basicBoard = new char[,]
//        {
//                { 'a','b','c'},
//                { 'a','b','c'},
//                { 'a','b','c'},
//        };
//
//        [Test]
//        public void BaseCaseGoesOffGrid()
//        {
//            BasicPrimaryWordSearcher basicPrimaryWordSearcher = new BasicPrimaryWordSearcherBuilder().Build();
//
//            List<Point> visitedPoints = new List<Point>();
//            List<string> foundWords = new List<string>();
//
//            basicPrimaryWordSearcher.Search(visitedPoints, 1, -1, 0, "asd", _basicBoard, foundWords, new Dictionary<string, List<string>>());
//
//            foundWords.Should().BeEmpty();
//        }
//    }
//}