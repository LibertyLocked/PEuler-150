/*
 * Solution for
 * Project Euler, Problem 150
 * 
 * Author: Wenchao Wang
 * 
 * I wrote it as close to assembly as possible, for an easy translation.
 * It uses an 1D array to represent the triangle.
 * 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEuler_150
{
    public class PE150Solver
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PE150Solver() { }


        /// <summary>
        /// Returns the minimum sum of a sub-triangle.
        /// </summary>
        /// <returns></returns>
        public int Solve(int depth)
        {
            // Parameter check
            if (depth <= 0) throw new InvalidOperationException("Invalid depth!");


            // Local variables
            int elementCount = GetElementCount(depth);  // The total number of elements
            int[] triangle = new int[elementCount];     // A 1D array representation of the triangle

            // Populate triangle array with random numbers
            // TODO: Use LCG to generate pseudo-random numbers

            // TEST CASE 1: (depth 4)
            // test: .word 4, 0, -3, -4, 1, 7, 2, 3, 5, 6, 7
            // sol: .word -7
            triangle = new int[] { 0, -3, -4, 1, 7, 2, 3, 5, 6, 7 };



            int currRow = 1;                            // Current row number. Starts at 1.
            int currRowFst = 0;                         // the index of the first number on currRow
            int minSum = triangle[0];;                  // Minimum sum

            // loop thru all the rows
            do
            {
                int currRowI = 0; // the i'th number on currRow. Starts at 0.

                // loop thru all elements on currRow
                do
                {
                    int tmpRow = currRow;
                    int tmpRowFst = currRowFst + currRowI;

                    // loop thru all tmpRows under (and including) currRow
                    do
                    {
                        int tmpRowI = 0;

                        // loop thru all relative elements on tmpRow
                        do
                        {

                        }
                        while (tmpRowI <= (tmpRow - currRow) + currRowI);

                        tmpRowFst += tmpRow;
                        tmpRow++;
                    }
                    while (tmpRow <= depth);

                }
                while (currRowI < currRow);

                currRowFst += currRow;
                currRow++;
            }
            while (currRow <= depth);


            // Return minimum sum
            return minSum;
        }


        private int GetElementCount(int depth)
        {
            // The array carries depth*(depth+1)*0.5 number of elements
            return (int)(depth * (depth + 1) * 0.5);
        }
    }
}
