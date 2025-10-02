using System;
namespace Task3
{
    public class Program
    {
        public static string ClassifyAge(int age)
        {
            if (age < 0 || age > 120) return "Нереальний вік";
            if (age <= 11) return "Ви дитина";
            if (age <= 17) return "Підліток";
            if (age <= 59) return "Дорослий";
            return "Пенсіонер";
        }
    }
}