namespace Filter
{
    public delegate bool FilterPredicate(int number);

    public static class Filter
    {
        public static void Run()
        {
            System.Console.WriteLine("=== Завдання 3: Фільтрація списку ===");

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            System.Console.Write("Парні числа: ");
            FilterArray(numbers, IsEven);
            System.Console.WriteLine();

            System.Console.Write("Числа > 5: ");
            FilterArray(numbers, IsGreaterThanFive);
            System.Console.WriteLine();

            System.Console.Write("Непарні числа (лямбда): ");
            FilterArray(numbers, n => n % 2 != 0);
            System.Console.WriteLine();
            System.Console.WriteLine();
        }

        public static void FilterArray(int[] numbers, FilterPredicate predicate)
        {
            foreach (int number in numbers)
            {
                if (predicate(number))
                {
                    System.Console.Write($"{number} ");
                }
            }
        }

        private static bool IsEven(int number) => number % 2 == 0;

        private static bool IsGreaterThanFive(int number) => number > 5;
    }
}