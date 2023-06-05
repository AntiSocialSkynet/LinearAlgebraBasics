using System.Data;
using System.Runtime.CompilerServices;

namespace LinearAlgebraBasics
{
    /// <summary>
    /// Class equipped with methods to handle the operation of Gaussian ELimination on an arbitrary n x m matrix containing floats.
    /// </summary>
    public class GaussianElimination
    {
        /// <summary>
        /// Provides the implementation of a row interchange on a 2 dimensional matrix in a static context.         
        /// </summary>
        /// <param name="matrix"></param> A 2-dimensional matrix operated upon
        /// <param name="row1"></param> the row index of the first row in the swap
        /// <param name="row2"></param> the row index of the second row in the swap
        /// <returns> the parameter matrix with the row swap implemented.</returns>
        public static float[,] RowInterchange(float[,] matrix, int row1, int row2)
        {
            float Temp;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                Temp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];//The first row swap should be handled here.
                matrix[row2, i] = Temp;//And the second
            }
            return matrix;
        }
        /// <summary>
        /// Provides implementation of row combination operations on a 2 dimensions matrix.
        /// </summary>
        /// <param name="matrix"></param> The 2-dimeitional matrix to be operated upon.
        /// <param name="row1"></param> The row index of the stationary row
        /// <param name="row2"></param> The row index of the row to be added
        /// <param name="coefficient"></param> The scalar of the row to be added
        /// <returns></returns>
        public static float[,] RowCombination(float[,] matrix, int row1, int row2, float coefficient)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[row1, i] = matrix[row1, i] + matrix[row2, i] * coefficient;
            }
            return matrix;
        }

        /// <summary>
        /// A method for scaling a row on a matrix given the row index and a coefficient. The coefficient should not be 0.
        /// </summary>
        /// <param name="matrix"></param> The matrix to be operated upon
        /// <param name="row"></param> The row to be multiplied 
        /// <param name="coefficient"></param> The scalar of the row. Should be non-zero.
        /// <returns></returns>
        public static float[,] RowScaling(float[,] matrix, int row, float coefficient)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
                matrix[row, i] *= coefficient;
            return matrix;
        }

        /// <summary>
        /// Returns a matrix in echelon form.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static float[,] EchelonForm(float[,] matrix)
        {
            int ExistantPivots = 0;            
            //scan across the columns of the matrix
            for(int j = ExistantPivots; j < matrix.GetLength(1); j++)
            {
                for (int i = ExistantPivots; i < matrix.GetLength(0); i++)
                {
                    if (!matrix[i, j].Equals( 0))
                    {
                        //once a pivot is found, clear out the column below the pivot. c begins on the pivot row and will count down the column
                        RowInterchange(matrix, i, ExistantPivots);
                        for (int c = ExistantPivots + 1; c < matrix.GetLength(0); c++)
                        {
                            if (!matrix[c, j].Equals(0))
                                RowCombination(matrix, c, ExistantPivots, (-1) * matrix[c, j] / matrix[ExistantPivots, j]);
                        }
                        ExistantPivots++;
                        break;
                    }
                }
            }

            return matrix;
        }
       /// <summary>
       /// Provides a Reduced Echelon Form by eliminating entries above the pivots in an Echelon Form.
       /// </summary>
       /// <param name="matrix"></param>
       /// <returns></returns>
        public static float[,] ReducedEchelonForm(float[,] matrix)//strategy: since each row can contain at most 1 pivot, this function works rowise
        {
            float[,] reducedMatrix = EchelonForm(matrix);
            for (int i = 0; i < reducedMatrix.GetLength(0);i++)
            {
                for(int j = i; j < reducedMatrix.GetLength(1); j++)//We assume the Echelon Form has zeros below a pivot.
                {
                    if (reducedMatrix[i,j] != 0)//the first non-zero entry is the pivot for the given row i
                    {
                        RowScaling(reducedMatrix, i, 1 / reducedMatrix[i, j]); //normalize the pivot
                        for(int k = 0; k < j; k++)
                        {
                            RowCombination(reducedMatrix, k, j, (-1) * reducedMatrix[k, j]); //create a 0 
                        }
                        break;
                    }
                }
            }
            return reducedMatrix;
        }
    }
}