using System;
using System.Xml;
using System.Xml.Linq;
using Лаб2_Сортировка_строк;

namespace SortingAlgorithms
{
    /// <summary>
    /// Represents a sorting algorithm implementation
    /// </summary>
    public class BubbleSort
    {
        /// <summary>
        /// Sorts an array using bubble sort algorithm
        /// </summary>
        /// <param name="array">Array to sort</param>
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

    /// <summary>
    /// Represents array generation strategies
    /// </summary>
    public class ArrayGenerator
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Generates an array based on arithmetic progression parameters
        /// </summary>
        public int[] GenerateArithmeticArray(int length, int minElement, int maxElement, int repeat)
        {
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = _random.Next(minElement, maxElement + 1);
            }
            return (BubbleSort.Sort(array));
        }

        /// <summary>
        /// Generates an array based on geometric progression parameters
        /// </summary>
        public int[] GenerateGeometricArray(int length, int minElement, int maxElement, int znamen, int repeat)
        {
            int[] array = new int[length];
            for (int j = 0; j < repeat; j++)
            {
                int current = minElement;
                for (int i = 0; i < length && current <= maxElement; i++)
                {
                    array[i] = current;
                    current *= znamen;
                }
            }
            return (BubbleSort.Sort(array));
        }
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

                    int[] array;
                    if (name.Contains("Arithmetic"))
                    {
                        array = generator.GenerateArithmeticArray(startLength, minElement, maxElement);
                    }
                    else // Geometric
                    {
                        int znamen = int.Parse(node.Attribute("Znamen")?.Value ?? "2");
                        array = generator.GenerateGeometricArray(startLength, minElement, maxElement, znamen, repeat);
                    }

                    Console.WriteLine($"Initial array: {string.Join(" ", array)}");

                    Console.WriteLine($"Sorted array:  {string.Join(" ", array)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}