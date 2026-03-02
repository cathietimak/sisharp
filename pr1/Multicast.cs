namespace Multicast
{
    public delegate void NotificationHandler(string message);

    public static class Multicast
    {
        public static void Run()
        {
            System.Console.WriteLine("=== Завдання 2: Мультикастинг ===");

            NotificationHandler? handler = null;
            handler += SendEmail;
            handler += SendSms;

            handler?.Invoke("Вітаємо! Практична робота виконана.");
            System.Console.WriteLine();
        }

        private static void SendEmail(string message)
        {
            System.Console.WriteLine($"Email sent: {message}");
        }

        private static void SendSms(string message)
        {
            System.Console.WriteLine($"SMS sent: {message}");
        }
    }
}