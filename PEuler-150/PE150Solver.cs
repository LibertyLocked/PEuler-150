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
        public static int SolveTestcase()
        {
            int depth;
            int[] triangle;

            // TEST CASE 1: (depth 4, sol -7)
            //depth = 4;
            //triangle = new int[] { 0, -3, -4, 1, 7, 2, 3, 5, 6, 7 };

            // TEST CASE 2: (depth 6, sol -42)
            depth = 6;
            triangle = new int[] { 15, -14, -7, 20, -13, -5, -3, 8, 23, -26, 1, -4, -5, -18, 5, -16, 31, 2, 9, 28, 3 };

            return SolveDataSet(depth, triangle);
        }

        /// <summary>
        /// Returns the min sum of a sub-triangle in a randomly generated data triangle.
        /// </summary>
        /// <param name="depth">Depth of the data triangle</param>
        /// <returns>Min sum</returns>
        public static int SolveInputs(int depth)
        {
            // Data set
            int elementCount = GetElementCount(depth);  // The total number of elements
            int[] triangle = new int[elementCount];     // A 1D array representation of the triangle

            // Populate triangle array with random numbers
            /*
             * t := 0 
               for k = 1 up to k = 500500: 
                    t := (615949*t + 797807) modulo 2^20 
                    sk := t−2^19
             */
            int t = 0;
            int k = 0;

            do
            {
                t = (615949 * t + 797807) % (1 << 20);
                triangle[k] = t - (1 << 19);
                k++;
            }
            while (k < elementCount);

            return SolveDataSet(depth, triangle);
        }

        /// <summary>
        /// Returns the min sum of a sub-triangle in a specified data triangle.
        /// </summary>
        /// <param name="inputs">Depth, then data in the triangle</param>
        /// <returns>Min sum</returns>
        public static int SolveInputs(params int[] inputs)
        {
            int depth = inputs[0];
            int[] triangle = new int[GetElementCount(depth)];
            for (int i = 0; i < triangle.Length; i++)
            {
                triangle[i] = inputs[i + 1];
            }

            return SolveDataSet(depth, triangle);
        }

        private static int SolveDataSet(int depth, int[] triangle)
        {
            // Solver code
            int currRow = 1;                            // Current row number. Starts at 1.
            int currRowFst = 0;                         // the index of the first number on currRow
            int sumMin = triangle[0]; ;                  // Minimum sum

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
