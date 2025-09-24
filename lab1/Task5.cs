using System;

namespace Task5
{
    public class Program
    {
        public static double GetAverage(int[] marks)
        {
            if (marks == null || marks.Length == 0) return 0;
            long sum = 0;
            foreach (var m in marks) sum += m;
            return (double)sum / marks.Length;
        }

        public static int GetMin(int[] marks)
        {
            if (marks == null || marks.Length == 0)
                throw new ArgumentException("Масив порожній");
            int min = marks[0];
            for (int i = 1; i < marks.Length; i++)
                if (marks[i] < min) min = marks[i];
            return min;
        }

        public static int GetMax(int[] marks)
        {
            if (marks == null || marks.Length == 0)
                throw new ArgumentException("Масив порожній");
            int max = marks[0];
            for (int i = 1; i < marks.Length; i++)
                if (marks[i] > max) max = marks[i];
            return max;
        }

        public static void PrintGroupStatistics(int[][] groups)
        {
            if (groups == null || groups.Length == 0)
            {
                Console.WriteLine("Немає груп.");
                return;
            }

            for (int i = 0; i < groups.Length; i++)
            {
                var marks = groups[i];
                if (marks == null || marks.Length == 0)
                {
                    Console.WriteLine($"Група {i + 1}: Немає оцінок");
                    continue;
                }

                double avg = GetAverage(marks);
                int min = GetMin(marks);
                int max = GetMax(marks);

                Console.WriteLine($"Група {i + 1}: Середній = {Math.Round(avg)}, Мінімальний = {min}, Максимальний = {max}");
            }
        }
    }
}