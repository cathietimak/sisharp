namespace Validator
{
    public delegate bool ValidatorDelegate(string text);

    public static class Validator
    {
        public static void Run()
        {
            Console.WriteLine("=== Завдання 6: Динамічний валідатор ===");

            ValidatorDelegate passwordValidator = GetValidator(8);
            ValidatorDelegate loginValidator = GetValidator(3);

            Console.Write("Введіть логін (мін. 3 символи): ");
            string login = Console.ReadLine() ?? string.Empty;

            Console.Write("Введіть пароль (мін. 8 символів): ");
            string password = Console.ReadLine() ?? string.Empty;

            Console.WriteLine($"Логін \"{login}\" валідний: {loginValidator(login)}");
            Console.WriteLine($"Пароль \"{password}\" валідний: {passwordValidator(password)}");

            string[] testValues = { "ab", "user", "1234567", "StrongPass1" };
            Console.WriteLine("Додаткові перевірки:");
            foreach (string value in testValues)
            {
                Console.WriteLine(
                    $"\"{value}\": login={loginValidator(value)}, password={passwordValidator(value)}");
            }

            Console.WriteLine();
        }

        public static ValidatorDelegate GetValidator(int minLength)
        {
            return text => !string.IsNullOrEmpty(text) && text.Length >= minLength;
        }
    }
}