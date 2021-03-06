﻿using System;

namespace WordBrainSolver.Core.Models
{
    [Serializable]
    public class Point
    {
        private readonly int _y;
        private readonly int _x;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X()
        {
            return _x;
        }

        public int Y()
        {
            return _y;
        }

        public bool HasValue(int x, int y)
        {
            return x == _x && y == _y;
        }

        public override string ToString()
        {
            return _x + "," + _y;
        }

        public override bool Equals(object obj)
        {
            return ((Point) obj).X() == X() && ((Point) obj).Y() == Y();
        }
    }
}