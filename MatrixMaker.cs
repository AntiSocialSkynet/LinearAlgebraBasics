﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebraBasics
{
    /// <summary>
    /// Class which provides basic methods for matrix generation.
    /// </summary>
   public class MatrixMaker
    {
        /// <summary>
        /// Generates a random matrix of dimensions n x m
        /// </summary>
        /// <param name="rows"></param> the numbers of rows, n, of the matrix
        /// <param name="cols"></param> the numbers of columns, m, of the matrix
        /// <param name="lowerBound"></param> the minimum value tolerated in the entires of the matrix.
        /// <param name="upperBound"></param> the maximum value tolerated in the entries of the matrix
        /// <returns>matrix with random entiries within the bounds set and of the specified dimensions</returns>
        public float[,] RandomMatrix(int rows,int cols, float lowerBound, float upperBound)
        {
            Random gen = new Random(); //gen is short for 'generator'
            float[,] matrix = new float[rows, cols];
            float genBound;

            if (lowerBound < 0) //we must use a different interval to generate the numbers in the matrix depending upon if the lower bound is negative
            {
                genBound = upperBound + Math.Abs(lowerBound);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = (float)((gen.NextDouble() * genBound) + lowerBound);
                    }
                }
            }
            else
            {
                genBound = upperBound - lowerBound;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = (float)((gen.NextDouble() * genBound) + lowerBound);
                    }
                }
            }
            return matrix;
        }
        /// <summary>
        /// Generates a matrix with random integer values
        /// </summary>
        /// <param name="row"></param> the rows in the matrix
        /// <param name="col"></param> the number of columns in the matrix
        /// <param name="lowerBound"></param> the lowest integer element admitted
        /// <param name="upperBound"></param> the largest integer element admitted
        /// <returns>An n by m matrix with random integer entries</returns>
        public float[,] RandomMatrix(int row, int col, int lowerBound, int upperBound)
        {
            Random gen = new Random();
            float[,] matrix = new float[row, col];
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    matrix[i, j] = (float)(gen.Next(lowerBound, upperBound));
                }
            }
            return matrix;
        }
        /// <summary>
        /// Generates the n x n identity matrix
        /// </summary>
        /// <param name="dim"></param> integer the dimension of the matrix
        /// <returns></returns>        
        public float[,] IdentityMatrix(int dim)
        {
            float[,] matrix = new float[dim, dim];
            for(int i = 0; i < dim; i++)
            {
                matrix[i, i] = 1;
            }
            return matrix;
        }
        /// <summary>
        /// Method to print out a 2 dimensional matrix with tab-delimited elements.
        /// </summary>
        /// <param name="matrix"></param>
        public static void PrintMatrix(float[,] matrix)
        {
            for(int i =0;i<matrix.GetLength(0);i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();                
            }
        }
        
    }
}
