using System;
using System.Xml;
using System.Xml.Linq;
using Лаб2_Сортировка_строк;

namespace SortingAlgorithms
{
    public class BubbleSort
    {
        public static int[] Sort(int[] array)
        {
            var IfCount = 0;
            var SwapsCount = 0;
            for (int j = 1; j < array.Length; j++)
            {
                bool isSorted = true;
                for (int i = 0; i < array.Length - j; i++)
                {
                    IfCount++;
                    if (array[i] > array[i + 1])
                    {
                        SwapsCount++;
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        isSorted = false;
                    }
                }
                if (isSorted)
                    break;
            }
            return [IfCount, SwapsCount];
        }
    }

    public class ArrayGenerator
    {
        private readonly Random _random = new Random();

        public int[] GetOperationsCount(int length, int minElement, int maxElement, int repeat, int maxLength)
        {
            int[] result = [2];
            int[] array = new int[length];
            for (int j = 0; j < repeat; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    array[i] = _random.Next(minElement, maxElement + 1);
                }
            result[0] += BubbleSort.Sort(array)[0];
            result[1] += BubbleSort.Sort(array)[0];
            }

            return (result);
        }

    public class Program
    {
        static void Main()
        {
            try
            {
                var xmlDoc = XDocument.Load("main.xml");
                var generator = new ArrayGenerator();

                foreach (var node in xmlDoc.Descendants("nodes"))
                {
                    string name = node.Attribute("name")?.Value ?? string.Empty;
                    int startLength = int.Parse(node.Attribute("startLength")?.Value ?? "0");
                    int minElement = int.Parse(node.Attribute("minElement")?.Value ?? "0");
                    int maxElement = int.Parse(node.Attribute("maxElement")?.Value ?? "0");
                    int repeat = int.Parse(node.Attribute("repeat")?.Value ?? "1");

                    Console.WriteLine($"\nProcessing {name} with length {startLength}:");

                    int[] result;
                    if (name.Contains("Arithmetic"))
                    // {
                    //     result = generator.GetOperationsCount(startLength, minElement, maxElement, repeat);
                    // }
                    // else
                    // {
                    //     int znamen = int.Parse(node.Attribute("Znamen")?.Value ?? "2");
                    //     result = generator.GenerateGeometricArray(startLength, minElement, maxElement, znamen, repeat);
                    // }

                    //Console.WriteLine($"Initial array: {string.Join(" ", array)}");

                    //Console.WriteLine($"Sorted array:  {string.Join(" ", array)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
}