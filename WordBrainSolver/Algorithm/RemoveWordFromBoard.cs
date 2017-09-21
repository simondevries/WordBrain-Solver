using System;
using System.Collections.Generic;
using WordBrainSolver.Core.Interfaces;
using WordBrainSolver.Core.Models;

namespace WordBrainSolver.Core.Algorithm
{
    /// <summary>
    /// Algorithm for removing words
    /// </summary>
    public class RemoveWordFromBoard : IRemoveWordFromBoard
    {
        /// <summary>
        /// Removes a word from the board and then moves shuffles everything down according to gravity
        /// </summary>
        public char[,] RemoveWords(char[,] inputBoard, List<Point> positionsToRemove, int gridSize)
        {
            char[,] newArray = new char[gridSize, gridSize];
            Array.Copy(inputBoard, newArray, gridSize * gridSize);

            //todo (sdv) ensure point stats with 0
            foreach (Point point in positionsToRemove)
            {
                newArray[point.X(), point.Y()] = '*';
            }

            //todo (sdv) should this be - 2?
            for (int rowNumber = gridSize - 2; rowNumber > -1; rowNumber--) // No need to check last row as it is already in place
            {
                for (int columnNumber = 0; columnNumber < gridSize; columnNumber++)
                {

                    if (newArray[rowNumber, columnNumber] != '*')
                    {
                        for (int i = rowNumber; i < gridSize -1; i++)
                        {
                            if (newArray[i + 1, columnNumber] == '*')
                            {
                                char tempStore = newArray[i, columnNumber];
                                newArray[i, columnNumber] = '*';
                                newArray[i + 1, columnNumber] = tempStore;
                            }
                        }
                    }

                }
            }



            //Move down
            //            for (int rowNumber = 0; rowNumber < gridSize - 1; rowNumber++) // Minus one because we dont want to search the bottom row
            //            {
            //                for (int columnNumber = 0; columnNumber < gridSize; columnNumber++)
            //                {
            //                    if (newArray[rowNumber + 1, columnNumber] == '*')
            //                    {
            //                        char tempStore = newArray[rowNumber, columnNumber];
            //                        newArray[rowNumber, columnNumber] = '*';
            //                        newArray[rowNumber + 1, columnNumber] = tempStore;
            //                    }
            //                }
            //            }

            return newArray;
        }
    }
}