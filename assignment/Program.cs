using System;

namespace VectorMatrixTransformations
{
    class Program
    {
        static void Main(string[] args)
        {
            // Z-axis rotation
            Console.Write("Enter the Z-axis rotation angle (in degrees): ");
            double rotationAngle = double.Parse(Console.ReadLine()!);

            double radians = rotationAngle * Math.PI / 180.0;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);

            double[,] rotationMatrixElements = new double[4, 4]
            {
                { cos, -sin, 0, 0 },
                { sin, cos, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            Matrix rotationMatrix = new Matrix(rotationMatrixElements);

            // Vector rotation
            Console.Write("Enter the vector (x,y,z) to be rotated: ");
            string[] vectorValues = Console.ReadLine()!.Split(',');
            double x = double.Parse(vectorValues[0]);
            double y = double.Parse(vectorValues[1]);
            double z = double.Parse(vectorValues[2]);

            Vector vector = new Vector(x, y, z);

            // Output original vector and rotation matrix
            Console.WriteLine("Original Vector:");
            Console.WriteLine($"x: {vector.X}, y: {vector.Y}, z: {vector.Z}");

            Console.WriteLine("\nRotation Matrix:");
            rotationMatrix.Print();

            // Rotate the vector using the rotation matrix
            Vector rotatedVector = rotationMatrix * vector;
        
            // Output rotated vector
            Console.WriteLine("\nRotated Vector:");
            Console.WriteLine($"x: {Math.Round(rotatedVector.X, 2)}, y: {Math.Round(rotatedVector.Y, 2)}, z: {Math.Round(rotatedVector.Z, 2)}");

            // Scaling matrix
            Console.Write("\nEnter the scaling factors (Sx, Sy, Sz): ");
            string[] scalingValues = Console.ReadLine()!.Split(',');
            double sx = double.Parse(scalingValues[0]);
            double sy = double.Parse(scalingValues[1]);
            double sz = double.Parse(scalingValues[2]);

            double[,] scalingMatrixElements = new double[4, 4]
            {
                { sx, 0, 0, 0 },
                { 0, sy, 0, 0 },
                { 0, 0, sz, 0 },
                { 0, 0, 0, 1 }
            };

            Matrix scalingMatrix = new Matrix(scalingMatrixElements);

            // Output scaling matrix
            Console.WriteLine("\nScaling Matrix:");
            scalingMatrix.Print();

            // Combined transformation matrix
            Matrix combinedMatrix = scalingMatrix * rotationMatrix;

            // Output combined transformation matrix
            Console.WriteLine("\nCombined Transformation Matrix:");
            combinedMatrix.Print();
        }
    }
}
