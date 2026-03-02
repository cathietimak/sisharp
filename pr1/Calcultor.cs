namespace Calculator
{
    public delegate double MathOperation(double a, double b);

    public static class Calculator
    {
        public static void Run()
        {
            System.Console.WriteLine("=== Завдання 1: Калькулятор ===");

            double a = 10;
            double b = 2;
            MathOperation operation;

            operation = Add;
            System.Console.WriteLine($"{a} + {b} = {operation(a, b)}");

            operation = Subtract;
            System.Console.WriteLine($"{a} - {b} = {operation(a, b)}");

            operation = Multiply;
            System.Console.WriteLine($"{a} * {b} = {operation(a, b)}");

            operation = Divide;
            System.Console.WriteLine($"{a} / {b} = {operation(a, b)}");
            System.Console.WriteLine();
        }

        private static double Add(double a, double b) => a + b;

        private static double Subtract(double a, double b) => a - b;

        private static double Multiply(double a, double b) => a * b;

        private static double Divide(double a, double b)
        {
            if (b == 0)
            {
                System.Console.WriteLine("Ділення на нуль неможливе.");
                return double.NaN;
            }

            return a / b;
        }
    }
}