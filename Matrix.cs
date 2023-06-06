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
   public class Matrix
    {
        /// <summary>
        /// Generates a random matrix of dimensions n x m
        /// </summary>
        /// <param name="rows"></param> the numbers of rows, n, of the matrix
        /// <param name="cols"></param> the numbers of columns, m, of the matrix
        /// <param name="lowerBound"></param> the minimum value tolerated in the entires of the matrix.
        /// <param name="upperBound"></param> the maximum value tolerated in the entries of the matrix
        /// <returns>matrix with random entiries within the bounds set and of the specified dimensions</returns>
        public static float[,] RandomMatrix(int rows,int cols, float lowerBound, float upperBound)
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
        public static float[,] RandomMatrix(int row, int col, int lowerBound, int upperBound)
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
        public static float[,] IdentityMatrix(int dim)
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
    
        /// <summary>
        /// Takes in a matrix element matrix 1 and augments another matrix element matrix 2 to the left of matrix 1. 
        /// Matrices 1 and 2 must have the same number of rows.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static float[,] AugmentMatrix(float[,] matrix1, float[,] matrix2)
        {
            float[,] augmentedMatrix = new float[matrix1.GetLength(0), matrix1.GetLength(1) + matrix2.GetLength(1)];
            //Add the columns together without adding any rows.
            for(int i = 0; i < matrix1.GetLength(0); i++)
            {
                for(int j = 0; j < matrix1.GetLength(1); j++)//populate the columns from the first matrix
                {
                    augmentedMatrix[i, j] = matrix1[i, j];
                }
                for(int k = matrix1.GetLength(1); k < matrix1.GetLength(1) + matrix2.GetLength(1); k++)//populate the columns from the second matrix
                {
                    augmentedMatrix[i, k] = matrix2[i, k - matrix1.GetLength(1)];
                }
            }
            
            return augmentedMatrix;
        }

        /// <summary>
        /// Defines element-by-element addition of two matrices matrix1 and matrix2
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>returns a matrix containing respective elements summed</returns>
        public static float[,] Add(float[,] matrix1, float[,] matrix2)
        {
            float[,] matrixSum = new float[matrix1.GetLength(0), matrix1.GetLength(1)];
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                    matrixSum[i, j] = matrix1[i, j] + matrix2[i, j];
            }
            return matrixSum;
        }
        /// <summary>
        /// Scales a given matrix by a given coefficient. The coefficient in question should not be zero, as that would invalidate many of the 
        /// valuable properties of a matrix and would be an irreversible operation. That is, the operation could not be reversed by dividing by zero.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="coefficient"></param>
        /// <returns></returns>
        public static float[,] Scale(float[,] matrix, float coefficient)
        {
            for (int i = 0; i < matrix.GetLength(0);i++) 
                GaussianElimination.RowScaling(matrix, i, coefficient);
            return matrix;
        }

        
        /// <summary>
        /// Provides a method for multiplying 2 floating point matrices by each other and returns the product. This method uses an implementation by 
        /// going entry by entry. We compute the inner product between each row and column, as this is the most general method for defining matrix 
        /// multiplication. In turn, the interior dimensions of the matrices should be equal. This is a core assumption in the definition of matrix 
        /// multiplication, and a failure to meet this precondition will likely result in an index out of range error.
        /// </summary>
        /// <param name="matrix1"></param> An n x m floating point matrix
        /// <param name="matrix2"></param> An m x p floating point matrix
        /// <returns> The n x p floating point matrix which is the product of the two matrices. </returns>
        public static float[,] Multiply(float[,] matrix1, float[,] matrix2)
        {
            float matrixEntry = 0;
            float[,] matrixProduct = new float[matrix1.GetLength(0), matrix2.GetLength(1)];
            //uses outer dimensions of matrices.
            for(int i = 0; i < matrix1.GetLength(0); i++)
            {
                for(int j = 0; j < matrix2.GetLength(1); j++) //we cycle across the product array, then compute entry by entry
                {
                    for(int k = 0; k < matrix1.GetLength(1);k++) //cycle across the interior dimensions of the matrix product.
                    {
                        matrixEntry += matrix1[i, k] * matrix2[k, j];   //compute the inner product between the row and column
                    }
                    matrixProduct[i, j] = matrixEntry;
                    matrixEntry = 0;    //assign and reset the matrix entry
                }
            }
            return matrixProduct;
        }

        /// <summary>
        /// Provide an implementation of matrix exponentiation. Matrix exponentiation is defined only for square matrices(matrices with the same number of 
        /// rows and columns, respectively). Exponentiation with non-square matrices may result in an error, as this implementation is based upon repeated 
        /// calls to the <seealso cref="Multiply(float[,], float[,])"/> function.
        /// </summary>
        /// <param name="matrix"></param> The n x n matrix which is desired to exponentiate.
        /// <param name="power"></param> The exponent to which the matrix will be raised. An power of 0 will return the n dimensional identity matrix.
        /// <returns></returns>
        public static float[,] Power(float[,] matrix, int power)
        {
            float[,] expMatrix = Matrix.IdentityMatrix(matrix.GetLength(0));
            for(int i = 0; i< power; i++) //if power = 0; loop is skipped. That means the identity matrix is returned.
            {
                expMatrix = Matrix.Multiply(expMatrix, matrix);//each power appends a new product
            }
            return expMatrix;
        }


    }
}
