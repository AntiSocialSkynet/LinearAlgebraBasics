using System.Data;

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
        public static float[,] RowInterchange(float[,] matrix,int row1, int row2)
        {
            float Temp;       
            for(int i = 0; i < matrix.GetLength(1); i++)
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
            for(int i = 0; i < matrix.GetLength(1); i++)
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
    }
}