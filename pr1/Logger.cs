namespace Logger
{
    public class LoggerService
    {
        public Action<string>? LogHandler;

        public void Log(string message)
        {
            LogHandler?.Invoke(message);
        }
    }

    public static class Logger
    {
        public static void Run()
        {
            Console.WriteLine("=== Завдання 5: Логування ===");

            var logger = new LoggerService();

            logger.LogHandler = message => Console.WriteLine($"[Console] {message}");
            logger.Log("Перший запис у звичайному форматі.");

            logger.LogHandler = message => Console.WriteLine($"[UPPER] {message.ToUpperInvariant()}");
            logger.Log("Другий запис після зміни обробника.");
            Console.WriteLine();
        }
    }
}