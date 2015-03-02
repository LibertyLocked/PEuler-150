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
        public int NodeProcessing
        {
            get;
            private set;
        }

        public int NodeTotal
        {
            get;
            private set;
        }

        public int SolveTestcase()
        {
            int depth;
            int[] triangle;

            // TEST CASE 1: (depth 4, sol -7)
            //depth = 4;
            //triangle = new int[] { 0, -3, -4, 1, 7, 2, 3, 5, 6, 7 };

            // TEST CASE 2: (depth 6, sol -42)
            depth = 6;
            triangle = new int[] { 15, -14, -7, 20, -13, -5, -3, 8, 23, -26, 1, -4, -5, -18, 5, -16, 31, 2, 9, 28, 3 };

            // TEST CASE 3: (depth 7, sol -488152)
            //depth = 7;
            //triangle = new int[] { 273519, -153582, 450905, 5108, 288723, -97242, 394845, -488152, 83831, 341882, 301473, 466844, -200869, 366094, -237787, 180048, -408705, 439266, 88809, 499780, -104477, 451830, 381165, -313736, -409465, -17078, -113359, 13804 };

            return SolveDataSet(depth, triangle);
        }

        /// <summary>
        /// Returns the min sum of a sub-triangle in a randomly generated data triangle.
        /// </summary>
        /// <param name="depth">Depth of the data triangle</param>
        /// <returns>Min sum</returns>
        public int SolveInputs(int depth)
        {
            // Data set
            int elementCount = GetElementCount(depth);  // The total number of elements
            int[] triangle = new int[elementCount];     // A 1D array representation of the triangle

            // Populate triangle array with random numbers (for 1000 rows, solution is -271248680)
            /*
             * t := 0 
               for k = 1 up to k = 500500: 
                    t := (615949*t + 797807) modulo 2^20 
                    sk := t−2^19
             */
            long t = 0;
            int k = 0;

            do
            {
                t = (615949 * t + 797807) % (1 << 20);
                triangle[k] = (int)(t - (1 << 19));
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
        public int SolveInputs(params int[] inputs)
        {
            int depth = inputs[0];
            int[] triangle = new int[GetElementCount(depth)];
            for (int i = 0; i < triangle.Length; i++)
            {
                triangle[i] = inputs[i + 1];
            }

            return SolveDataSet(depth, triangle);
        }

        private int SolveDataSet(int depth, int[] triangle)
        {
            // Set NodeTotal property
            NodeTotal = triangle.Length;

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
                    int tmpRow = currRow;
                    int tmpRowFst = currRowFst + currRowI;
                    int currElSum = 0;
                    int currElSumMin = 0;

                    // For debugging purposes only!
                    //Debug.WriteLine("Calculating min sum on apex [" + (currRowFst + currRowI) + "]");
                    //Console.WriteLine((currRowFst + currRowI + 1) + "/" + triangle.Length);
                    NodeProcessing = currRowFst + currRowI + 1;

                    // loop thru all tmpRows under (and including) currRow
                    do
                    {
                        int tmpRowI = 0;
                        int tmpRowSum = 0;  // stores the sum on this tmpRow only!

                        // loop thru all relative elements on tmpRow
                        do
                        {
                            tmpRowSum += triangle[tmpRowFst + tmpRowI];
                            //Debug.WriteLine("tmpRowSum += triangle[" + (tmpRowFst + tmpRowI) + "]");
                            tmpRowI++; // goto next element in tmpRow
                        }
                        while (tmpRowI <= (tmpRow - currRow));

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
