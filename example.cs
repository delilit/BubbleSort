using System;
using System.Linq;
using System.Xml;

namespace Лаб2_Сортировка_строк
{
    class Trying
    {
        public static void tryi(string[] args)
        {
            if (!Test())
                return;

            Console.Write("Выполнить эксперимент перед началом программы (yes/no)? ");
            var answer = Console.ReadLine();
            if (answer == "yes")
                GetExperimentResults();

            Console.Write("Введите строки для сортировки, разделяя их пробелом: ");
            var input = Console.ReadLine().Split();
            HeapSort(input);
            Console.Write("Отсортированный массив: ");
            PrintArray(input);
        }

        public static int HeapSort(string[] array)
        {
            var heapSize = array.Length;
            var swapsCount = 0;
            var branchesCount = 0;

            for (var i = heapSize / 2 - 1; i >= 0; i--)
                Heapify(array, heapSize, i, ref swapsCount, ref branchesCount);

            for (var i = heapSize - 1; i >= 0; i--)
            {
                var swap = array[0];
                array[0] = array[i];
                array[i] = swap;
                swapsCount++;
                Heapify(array, i, 0, ref swapsCount, ref branchesCount);
            }

            return swapsCount >= branchesCount ? swapsCount : branchesCount;
        }

        private static void Heapify(string[] array, int heapSize, int i, ref int swapsCount, ref int branchesCount)
        {
            var largestElem = i;
            var leftChild = 2 * i + 1;
            var rightChild = 2 * i + 2;

            if (leftChild < heapSize && String.Compare(array[leftChild], array[largestElem], StringComparison.Ordinal) > 0)
                largestElem = leftChild;

            if (rightChild < heapSize && String.Compare(array[rightChild], array[largestElem], StringComparison.Ordinal) > 0)
                largestElem = rightChild;

            if (largestElem != i)
            {
                var swap = array[i];
                array[i] = array[largestElem];
                array[largestElem] = swap;
                swapsCount++;
                Heapify(array, heapSize, largestElem, ref swapsCount, ref branchesCount);
            }

            branchesCount += 3;
        }

        private static void PrintArray(string[] array)
        {
            foreach (var item in array)
                Console.Write(item + " ");
            Console.Read();
        }

        private static void GetExperimentResults()
        {
            var xDoc = new XmlDocument();
            xDoc.Load(@"C:\Users\Artem\source\repos\Лаб2 Сортировка строк\Лаб2 Сортировка строк\Experiment.xml");
            var xRoot = xDoc.DocumentElement;
            var experiment = xRoot.SelectSingleNode("experiment");

            foreach (XmlNode node in experiment.ChildNodes)
            {
                var minElement = int.Parse(node.Attributes["minElement"].Value);
                var maxElement = int.Parse(node.Attributes["maxElement"].Value);
                var startLength = int.Parse(node.Attributes["startLength"].Value);
                var maxLength = int.Parse(node.Attributes["maxLength"].Value);
                var repeat = int.Parse(node.Attributes["repeat"].Value);
                var name = node.Attributes["name"].Value;

                if (name == "Arithmetic Progression")
                {
                    var diff = int.Parse(node.Attributes["diff"].Value);
                    Console.WriteLine("Эксперимент: " + name);

                    for (var length = startLength; length <= maxLength; length += diff)
                    {
                        var avgOps = GetOperationsCount(minElement, maxElement, repeat, length) / repeat;
                        Console.WriteLine($"Длина массива: {length}\tКол-во массивов: {repeat}\tОпераций в среднем: {avgOps}");
                    }
                }

                if (name == "Geometric Progression")
                {
                    var znamen = double.Parse(node.Attributes["Znamen"].Value);
                    Console.WriteLine("Эксперимент: " + name);

                    for (var length = startLength; length <= maxLength; length = (int)Math.Round(length * znamen))
                    {
                        var avgOps = GetOperationsCount(minElement, maxElement, repeat, length) / repeat;
                        Console.WriteLine($"Длина массива: {length}\tКол-во массивов: {repeat}\tОпераций в среднем: {avgOps}");
                    }
                }

                Console.WriteLine();
            }
        }

        private static int GetOperationsCount(int minElement, int maxElement, int repeatsCount, int arrayLength)
        {
            var operationsCount = 0;

            for (var i = 0; i < repeatsCount; i++)
            {
                var array = new string[arrayLength];
                var random = new Random();

                for (var j = 0; j < array.Length; j++)
                    array[j] = random.Next(minElement, maxElement).ToString();

                operationsCount += HeapSort(array);
            }

            return operationsCount;
        }

        private static bool Test()
        {
            var isAllTestsCompleted = true;

            void RunTest(string[] startArray, string[] expectedArray, int testNum)
            {
                HeapSort(startArray);
                if (!Enumerable.SequenceEqual(startArray, expectedArray))
                {
                    Console.WriteLine($"Test {testNum} wasn't passed");
                    isAllTestsCompleted = false;
                }
            }

            RunTest(new[] { "bdg", "avc", "hdf", "aa", "dg", "chd" }, new[] { "aa", "avc", "bdg", "chd", "dg", "hdf" }, 1);
            RunTest(new[] { "enfgh", "sgasf", "asdg", "ksd", "asa", "bhj", "ksgd", "assa", "bhdfj" }, new[] { "asa", "asdg", "assa", "bhdfj", "bhj", "enfgh", "ksd", "ksgd", "sgasf" }, 2);
            RunTest(new[] { "mbdsdfg", "avghssdc", "hjsdf", "abfga", "dhjsg", "lnvchd" }, new[] { "abfga", "avghssdc", "dhjsg", "hjsdf", "lnvchd", "mbdsdfg" }, 3);
            RunTest(new[] { "ajb", "aja", "agah", "agga", "aswgs", "aswgc", "aba", "aab" }, new[] { "aab", "aba", "agah", "agga", "aja", "ajb", "aswgc", "aswgs" }, 4);
            RunTest(new[] { "baggage", "auto", "holiday", "bug", "dog", "child" }, new[] { "auto", "baggage", "bug", "child", "dog", "holiday" }, 5);

            return isAllTestsCompleted;
        }
    }
}
