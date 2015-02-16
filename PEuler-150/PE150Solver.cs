/*
 * Solution for
 * Project Euler, Problem 150
 * 
 * Author: Wenchao Wang
 * 
 * It uses an 1D array to represent the triangle.
 * 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PEuler_150
{
    public class PE150Solver
    {
        /// <summary>
        /// Returns the minimum sum of a sub-triangle.
        /// </summary>
        /// <param name="depth">Total number of rows in the triangle.</param>
        /// <returns></returns>
        public static int Solve(int depth)
        {
            // Data set
            int elementCount = GetElementCount(depth);  // The total number of elements
            int[] triangle = new int[elementCount];     // A 1D array representation of the triangle

            // Populate triangle array with random numbers
            // TODO: Use LCG to generate pseudo-random numbers

            // TEST CASE 1: (depth 4, sol -7)
            //depth = 4;
            //triangle = new int[] { 0, -3, -4, 1, 7, 2, 3, 5, 6, 7 };

            // TEST CASE 2: (depth 6, sol -42)
            depth = 6;
            triangle = new int[] { 15, -14, -7, 20, -13, -5, -3, 8, 23, -26, 1, -4, -5, -18, 5, -16, 31, 2, 9, 28, 3 };

            // Solver code
            int currRow = 1;                            // Current row number. Starts at 1.
            int currRowFst = 0;                         // the index of the first number on currRow
            int sumMin = triangle[0];;                  // Minimum sum

            // loop thru all the rows
            do
            {
                int currRowI = 0; // the i'th number on currRow. Starts at 0.

                // loop thru all elements on currRow
                do
                {
                    Debug.WriteLine("Calculating min sum on apex [" + (currRowFst + currRowI) + "]");
                    int tmpRow = currRow;
                    int tmpRowFst = currRowFst + currRowI;
                    int currElSum = 0;
                    int currElSumMin = 0;

                    // loop thru all tmpRows under (and including) currRow
                    do
                    {
                        int tmpRowI = 0;
                        int tmpRowSum = 0;  // stores the sum on this tmpRow only!

                        // loop thru all relative elements on tmpRow
                        do
                        {
                            tmpRowSum += triangle[tmpRowFst + tmpRowI];
                            Debug.WriteLine("tmpRowSum += triangle[" + (tmpRowFst + tmpRowI) + "]");
                            tmpRowI++; // goto next element in tmpRow
                        }
                        while (tmpRowI <= (tmpRow - currRow)); // there's a problem with this - fix it later!

                        // add tmpRowSum to currElSum
                        currElSum += tmpRowSum;
                        // check if we have a new element sum min
                        if (currElSum < currElSumMin) currElSumMin = currElSum;

                        tmpRowFst += tmpRow;
                        tmpRow++; // goto next tmpRow
                    }
                    while (tmpRow <= depth);

                    // check if we have a new overall min
                    if (currElSumMin < sumMin) sumMin = currElSumMin;

                    currRowI++; // goto next element in currRow
                }
                while (currRowI < currRow);

                currRowFst += currRow;
                currRow++; // goto next row
            }
            while (currRow <= depth);

            // Return minimum sum
            return sumMin;
        }


        private static int GetElementCount(int depth)
        {
            // The array carries depth*(depth+1)*0.5 number of elements
            return (int)(depth * (depth + 1) * 0.5);
        }
    }
}
