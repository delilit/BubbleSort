using System;
using System.Xml;
using System.Xml.Linq;
//using Лаб2_Сортировка_строк;

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

        public int[] GetOperationsCount(int length, int minElement, int maxElement)
        {
            int[] result = new int[2];
            int[] array = new int[length];
                for (int i = 0; i < length; i++)
                {
                    array[i] = _random.Next(minElement, maxElement + 1);
                }
            int[] sortResult = BubbleSort.Sort(array);
            return (sortResult);
        }

    public class Program
    {
        static void Experiments()
        {
            try
            {
                var xmlDoc = XDocument.Load("main.xml");
                var generator = new ArrayGenerator();

                foreach (var node in xmlDoc.Descendants("nodes"))
                {
                    string name = node.Attribute("name")?.Value ?? string.Empty;
                    int startLength = int.Parse(node.Attribute("startLength")?.Value ?? "0");
                    int maxLength = int.Parse(node.Attribute("maxLength")?.Value ?? "0");
                    int minElement = int.Parse(node.Attribute("minElement")?.Value ?? "0");
                    int maxElement = int.Parse(node.Attribute("maxElement")?.Value ?? "0");
                    int repeat = int.Parse(node.Attribute("repeat")?.Value ?? "1");
                    int[] result;
                    if (name.Contains("Arithmetic"))
                    {
                        Console.WriteLine("Arithmetic progression:");
                        int diff = int.Parse(node.Attribute("diff")?.Value ?? "0");
                        for( var length = startLength; length <= maxLength; length+= diff)
                        {
                            for (int i = 1; i<repeat; i++){
                            result = generator.GetOperationsCount(length, minElement, maxElement);
                            Console.WriteLine($"{length,6}, {result[1],8}");
                        }
                        }
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        Console.WriteLine("Geometrical progression:");
                        int znamen = int.Parse(node.Attribute("Znamen")?.Value ?? "2");
                        for( var length = startLength; length <= maxLength; length*= znamen)
                        {
                            for (int i = 1; i<repeat; i++){
                            result = generator.GetOperationsCount(length, minElement, maxElement);
                            Console.WriteLine($"{length,6}, {result[1],8}");
                        }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static void Main()
        {
            if (testing() == false){
                return;
            }
                        Console.Write("Выполнить эксперимент перед началом программы (yes/no)? ");
            var answer = Console.ReadLine();
            if (answer == "yes")
                Experiments();
            
            while (true){
            Console.Write("Введите строки для сортировки, разделяя их пробелом или 'Enter' для завершения: ");

            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                return;

            var input = line.Split();
            if (input.SequenceEqual(new string[] {""})){
                return;
            }
            var array = new int[input.Length];
            for (int i = 0; i < input.Length; i++){
                array[i] = Int32.Parse(input[i]);
            }
            BubbleSort.Sort(array);
            Console.Write("Отсортированный массив: ");
            foreach (var item in array) Console.Write(item + " ");
            Console.Write("\n\n");
            }
        }
    }


    static bool testing(){

        //Test 1

        bool is_passed = true;
        var array = new int[] { 5, 4, 3, 2, 1 };
        var true_array = new int[] {1, 2, 3, 4, 5};
        BubbleSort.Sort(array);
        
        if (!array.SequenceEqual(true_array)){
            Console.WriteLine($"Test 1 isn't passed.");
            Console.WriteLine("Actual:");
            foreach (var item in array) Console.Write(item + " ");
            Console.Write("\n\n");

            Console.WriteLine("Expected:");
            foreach (var item in true_array) Console.Write(item + " ");
            Console.Write("\n\n");

            is_passed = false;
        }

        //Test 2
        array = new int[]{1, 2, 3, 4, 5};
        true_array = new int[] {1, 2, 3, 4, 5};
        BubbleSort.Sort(array);
        
        if (!array.SequenceEqual(true_array)){
            Console.WriteLine($"Test 2 isn't passed.");
            Console.WriteLine("Actual:");
            foreach (var item in array) Console.Write(item + " ");
            Console.Write("\n\n");

            Console.WriteLine("Expected:");
            foreach (var item in true_array) Console.Write(item + " ");
            Console.Write("\n\n");

            is_passed = false;
        }

        //Test 3
        array = new int[]{};
        true_array = new int[] {};
        BubbleSort.Sort(array);
        
        if (!array.SequenceEqual(true_array)){
            Console.WriteLine($"Test 3 isn't passed.");
            Console.WriteLine("Actual:");
            foreach (var item in array) Console.Write(item + " ");
            Console.Write("\n\n");

            Console.WriteLine("Expected:");
            foreach (var item in true_array) Console.Write(item + " ");
            Console.Write("\n\n");

            is_passed = false;
        }      

        //Test 4

        array = new int[]{99999999,9999999,1,0,-100};
        true_array = new int[] {-100, 0, 1, 9999999, 99999999};
        BubbleSort.Sort(array);
        
        if (!array.SequenceEqual(true_array)){
            Console.WriteLine($"Test 3 isn't passed.");
            Console.WriteLine("Actual:");
            foreach (var item in array) Console.Write(item + " ");
            Console.Write("\n\n");

            Console.WriteLine("Expected:");
            foreach (var item in true_array) Console.Write(item + " ");
            Console.Write("\n\n");

            is_passed = false;
        }      

        //Test 5

        array = new int[]{99999999,9999999, 1,0,-100};
        int[] sorted_count = BubbleSort.Sort(array);
        
        if (sorted_count[0] != 10 || sorted_count[1] != 10){
            Console.WriteLine($"Test 3 isn't passed.");
            Console.WriteLine($"Actual: {sorted_count[0]}, {sorted_count[1]}");

            Console.WriteLine("Expected: 10");
            is_passed = false;
        }   
        // Test 6: Все элементы одинаковые
    array = new int[]{7, 7, 7, 7, 7};
    true_array = new int[]{7, 7, 7, 7, 7};
    BubbleSort.Sort(array);

    if (!array.SequenceEqual(true_array)){
        Console.WriteLine("Test 6 isn't passed.");
        is_passed = false;
    }

    // Test 7: Один элемент
    array = new int[]{42};
    true_array = new int[]{42};
    BubbleSort.Sort(array);

    if (!array.SequenceEqual(true_array)){
        Console.WriteLine("Test 7 isn't passed.");
        is_passed = false;
    }

    // Test 8: Два элемента, не отсортированы
    array = new int[]{2, 1};
    true_array = new int[]{1, 2};
    BubbleSort.Sort(array);

    if (!array.SequenceEqual(true_array)){
        Console.WriteLine("Test 8 isn't passed.");
        is_passed = false;
    }

    // Test 9: Отрицательные числа
    array = new int[]{-3, -1, -7, -5};
    true_array = new int[]{-7, -5, -3, -1};
    BubbleSort.Sort(array);

    if (!array.SequenceEqual(true_array)){
        Console.WriteLine("Test 9 isn't passed.");
        is_passed = false;
    }

    // Test 10: Большой массив на корректность длины и сортировки
    array = Enumerable.Range(1, 1000).Reverse().ToArray();
    true_array = Enumerable.Range(1, 1000).ToArray();
    BubbleSort.Sort(array);

    if (!array.SequenceEqual(true_array)){
        Console.WriteLine("Test 10 isn't passed.");
        is_passed = false;
    }

    // Test 11: Проверка GetOperationsCount на возврат 2 элементов
    var generator = new ArrayGenerator();
    int[] operations = generator.GetOperationsCount(10, 1, 100);
    if (operations.Length != 2)
    {
        Console.WriteLine("Test 11 isn't passed (GetOperationsCount should return array of length 2).");
        is_passed = false;
    }

    // Test 12: Проверка, что количество сравнений и обменов >= 0
    if (operations[0] < 0 || operations[1] < 0)
    {
        Console.WriteLine("Test 12 isn't passed (negative operation counts).");
        is_passed = false;
    }
        return is_passed;

    }
    
}
}