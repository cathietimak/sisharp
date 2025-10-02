using System;
namespace Task2
{
    public class Program
    {
        public static int[] GenerateRandomArray(int size, int min, int max)
        {
            var rnd = new Random();
            var arr = new int[size];
            for (int i = 0; i < size; i++)
                arr[i] = rnd.Next(min, max + 1);
            return arr;
        }

        public static int GetSum(int[] numbers)
        {
            if (numbers == null) return 0;
            int sum = 0;
            foreach (var n in numbers) sum += n;
            return sum;
        }

        public static double GetAverage(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0) return 0;
            return (double)GetSum(numbers) / numbers.Length;
        }

        public static int GetMin(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentException("Масив порожній");

            int min = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] < min)
                    min = numbers[i];
            }
            return min;
        }

        public static int GetMax(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentException("Масив порожній");

            int max = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                    max = numbers[i];
            }
            return max;
        }
    }
}