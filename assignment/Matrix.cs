namespace VectorMatrixTransformations
{
    class Matrix
    {
        private double[,] elements;

        public Matrix(double[,] elements)
        {
            this.elements = elements;
        }

        public static Matrix IdentityMatrix()
        {
            double[,] identity = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    identity[i, j] = (i == j) ? 1.0 : 0.0;
                }
            }

            return new Matrix(identity);
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            int rowsM1 = m1.elements.GetLength(0);
            int colsM1 = m1.elements.GetLength(1);
            int colsM2 = m2.elements.GetLength(1);

            double[,] result = new double[rowsM1, colsM2];

            for (int i = 0; i < rowsM1; i++)
            {
                for (int j = 0; j < colsM2; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < colsM1; k++)
                    {
                        sum += m1.elements[i, k] * m2.elements[k, j];
                    }

                    result[i, j] = sum;
                }
            }

            return new Matrix(result);
        }

        public static Vector operator *(Matrix m, Vector v)
        {
            double x = m.elements[0, 0] * v.X + m.elements[0, 1] * v.Y + m.elements[0, 2] * v.Z + m.elements[0, 3];
            double y = m.elements[1, 0] * v.X + m.elements[1, 1] * v.Y + m.elements[1, 2] * v.Z + m.elements[1, 3];
            double z = m.elements[2, 0] * v.X + m.elements[2, 1] * v.Y + m.elements[2, 2] * v.Z + m.elements[2, 3];
            double w = m.elements[3, 0] * v.X + m.elements[3, 1] * v.Y + m.elements[3, 2] * v.Z + m.elements[3, 3];

            return new Vector(x / w, y / w, z / w);
        }

        public Matrix Inverse()
        {
            double[,] m = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m[i, j] = elements[i, j];
                }
            }

            double[,] result = new double[4, 4];
            int[] pivotIndices = new int[4];
            int[] rowIndices = new int[4];
            int[] colIndices = new int[4];

            for (int i = 0; i < 4; i++)
            {
                int iPivotRow = -1;
                int iPivotCol = -1;
                double dPivot = 0;

                // Find the largest pivot element
                for (int j = 0; j < 4; j++)
                {
                    if (rowIndices[j] != 1)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (colIndices[k] == 0)
                            {
                                if (Math.Abs(m[j, k]) >= dPivot)
                                {
                                    dPivot = Math.Abs(m[j, k]);
                                    iPivotRow = j;
                                    iPivotCol = k;
                                }
                            }
                            else if (colIndices[k] > 1)
                            {
                                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
                            }
                        }
                    }
                }

                if (dPivot == 0)
                {
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
                }

                // Mark the pivot row and column
                rowIndices[iPivotRow]++;
                colIndices[iPivotCol]++;

                // Swap the pivot element with the current row
                if (iPivotRow != iPivotCol)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        double temp = m[iPivotRow, k];
                        m[iPivotRow, k] = m[iPivotCol, k];
                        m[iPivotCol, k] = temp;
                    }
                }

                // Scale the pivot row
                double fPivotInverse = 1.0 / m[iPivotCol, iPivotCol];
                m[iPivotCol, iPivotCol] = 1.0;
                for (int k = 0; k < 4; k++)
                {
                    m[iPivotCol, k] *= fPivotInverse;
                }

                // Eliminate the pivot column elements in other rows
                for (int j = 0; j < 4; j++)
                {
                    if (j != iPivotCol)
                    {
                        double fCoeff = m[j, iPivotCol];
                        m[j, iPivotCol] = 0.0;
                        for (int k = 0; k < 4; k++)
                        {
                            m[j, k] -= m[iPivotCol, k] * fCoeff;
                        }
                    }
                }
            }

            for (int j = 3; j >= 0; j--)
            {
                int indxr = pivotIndices[j];
                int indxc = pivotIndices[j];

                if (indxr != indxc)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        double temp = result[k, indxr];
                        result[k, indxr] = result[k, indxc];
                        result[k, indxc] = temp;
                    }
                }
            }

            return new Matrix(result);
        }

        public void Print()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(Math.Round(elements[i, j], 2) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}