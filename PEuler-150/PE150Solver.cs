/*
 * Solution for
 * Project Euler, Problem 150
 * 
 * Author: Wenchao Wang
 * 
 * I wrote it as close to assembly as possible, for easy translation.
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
            int[] minSums = new int[elementCount];      // A 1D array storing min sum for each apex
            int currRow = 1;                            // Current row number. Starts at 1.
            int minSum = 0;                             // Minimum sum

            // Populate triangle array with random numbers
            // TODO: Use LCG to generate pseudo-random numbers

            // TEST CASE 1: (depth 4)
            // test: .word 4, 0, -3, -4, 1, 7, 2, 3, 5, 6, 7
            // sol: .word -7
            triangle = new int[] { 0, -3, -4, 1, 7, 2, 3, 5, 6, 7 };

            // Solver logic
            do
            {
                // Temp variables
                int currIndex = GetElementCount(currRow - 1);
                int tempRow = currRow;
                int currSum = 0;
                
                do
                {

                }
                while (tempRow <= depth);

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
