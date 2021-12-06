using System;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"C:\aoc_day1.txt").Select(line => Convert.ToInt32(line)).ToArray();
            var totalLength = lines.Length;
            var currentIndex = 0;

            var increasedCount = 0;
            var previousWindowSum = 0;

            while (currentIndex < totalLength - 2)
            {
                var window1 = lines[currentIndex];
                var window2 = lines[currentIndex + 1];
                var window3 = lines[currentIndex + 2];
                var currentWindowSum = window1 + window2 + window3;

                if (currentWindowSum > previousWindowSum) increasedCount++;
                previousWindowSum = currentWindowSum;
                currentIndex++;
            }

            Console.WriteLine($"Total increased count: {increasedCount - 1}");
        }
    }
}
