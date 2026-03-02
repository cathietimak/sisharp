namespace Standarddelegates
{
    public static class StandardDelegates
    {
        public static void Run()
        {
            Console.WriteLine("=== Завдання 4: Func та Action ===");

            double a = 12;
            double b = 3;
            Func<double, double, double> operation;

            operation = (x, y) => x + y;
            Console.WriteLine($"{a} + {b} = {operation(a, b)}");

            operation = (x, y) => x - y;
            Console.WriteLine($"{a} - {b} = {operation(a, b)}");

            operation = (x, y) => x * y;
            Console.WriteLine($"{a} * {b} = {operation(a, b)}");

            operation = (x, y) => y != 0 ? x / y : double.NaN;
            Console.WriteLine($"{a} / {b} = {operation(a, b)}");

            var students = new List<string>
            {
                "Андрій",
                "Анна",
                "Богдан",
                "Вікторія",
                "Аліна"
            };

            string startsWith = "А";
            Predicate<string> startsWithLetter = name => name.StartsWith(startsWith, StringComparison.OrdinalIgnoreCase);
            List<string> filteredStudents = students.FindAll(startsWithLetter);

            Console.WriteLine($"Імена на літеру '{startsWith}': {string.Join(", ", filteredStudents)}");
            Console.WriteLine();
        }
    }
}