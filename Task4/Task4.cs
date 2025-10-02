using System;
using System.Globalization;

namespace Task4
{
    public class Program
    {
        public static bool IsValidTriangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0) return false;
            return a + b > c && a + c > b && b + c > a;
        }

        public static double GetPerimeter(double a, double b, double c)
        {
            if (!IsValidTriangle(a, b, c)) throw new ArgumentException("Невалідний трикутник");
            return a + b + c;
        }

        public static double GetArea(double a, double b, double c)
        {
            if (!IsValidTriangle(a, b, c)) throw new ArgumentException("Невалідний трикутник");
            double s = GetPerimeter(a, b, c) / 2.0;
            double areaSquared = s * (s - a) * (s - b) * (s - c);
            return areaSquared <= 0 ? 0 : Math.Sqrt(areaSquared);
        }

        public static string GetTriangleType(double a, double b, double c)
        {
            if (!IsValidTriangle(a, b, c)) throw new ArgumentException("Невалідний трикутник");

            bool eqAB = a == b;
            bool eqAC = a == c;
            bool eqBC = b == c;

            if (eqAB && eqAC) return "рівносторонній";

            double[] sides = { a, b, c };
            Array.Sort(sides);
            double eps = 1e-9;
            if (Math.Abs(sides[0] * sides[0] + sides[1] * sides[1] - sides[2] * sides[2]) <= eps)
                return "прямокутний";

            if (eqAB || eqAC || eqBC) return "рівнобедрений";

            return "довільний";
        }
    }
}