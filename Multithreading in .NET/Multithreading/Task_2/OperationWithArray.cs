using System;

namespace Task_2
{
    class OperationWithArray
    {
        public static int[] CreateArray()
        {
            var array = new int[10];

            var random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 10);
                Console.Write($"{array[i]} ");
            }

            return array;
        }

        public static int[] MultipliesArray(int[] array)
        {
            var random = new Random();
            Console.WriteLine();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i] * random.Next(1, 10);
                Console.Write($"{array[i]} ");
            }

            return array;
        }

        public static int[] SortArray(int[] array)
        {
            int temp;
            Console.WriteLine();
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
                Console.Write($"{array[i]} ");
            }

            return array;
        }

        public static void CalculateAverageValue(int[] array)
        {
            int rezult = 0;
            foreach (var t in array)
            {
                rezult += t;
            }
            Console.WriteLine($"\nRezult = {rezult}");
        }
    }
}
