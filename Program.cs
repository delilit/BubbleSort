using System;
using System.Linq;
using System.Globalization;
using System.Runtime.CompilerServices;
class Program{

static void Main(){
Random rd = new Random();
int[] nums = new int[15];
for (int j = 0; j < nums.Length; j++)
{
    nums[j] = rd.Next(1, 20);
}

var l_1 = string.Join(" ", nums);  
Console.WriteLine($"Изначальный массив {l_1}");

var counts = Bubble(nums);

Console.WriteLine($"{counts[0]}, {counts[1]}");

var l_2 = string.Join(" ", nums);  
Console.WriteLine($"Изначальный массив {l_2}");


}
static int[] Bubble(int[] nums){

var swaps_count= 0;
int ifs_count = 0;



for (int j = 1; j < nums.Length; j++)
{
    bool IsSorted = true;
    for (int i = 0; i < nums.Length - j; i++)
    {
        ifs_count += 1;
        if (nums[i] > nums[i + 1])
        {
            swaps_count +=1;
            (nums[i], nums[i + 1]) = (nums[i + 1], nums[i]);
            IsSorted = false;
        }

    }
    if (IsSorted)
    {
        break;
    }

}
return [swaps_count,ifs_count];
}
static void Test(){
var isAllTestsCompleted = true;

var startArray = new[] {12,8,4,1,6,12,52};
var expectedArray = new[] {1,4,6,12,12,52};
Bubble(startArray);
if (!Enumerable.SequenceEqual(startArray, expectedArray))
{
Console.WriteLine("Test 1 wasn't passed");
isAllTestsCompleted = false;
}

   
}
}

