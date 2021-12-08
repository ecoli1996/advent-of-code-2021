using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
	{
		static void Main(string[] args)
		{
			var lines = System.IO.File.ReadAllLines(@"C:/aoc_day3.txt");

			var column = 0;
            var startWithOne = new List<string>();
            var startWithZero = new List<string>();
			var numberOfBits = lines[0].Length;

            foreach (string line in lines)
            {
                if (line[column] == '1') startWithOne.Add(line);
                else startWithZero.Add(line);
            }

			string calculateOx;
			string calculateScrubber;
			if (startWithZero.Count > startWithOne.Count) {
				calculateOx = CalculateOxygen(startWithZero, 1);
				calculateScrubber = CalculateScrubber(startWithOne, 1);
			} else {
				calculateOx = CalculateOxygen(startWithOne, 1);
				calculateScrubber = CalculateScrubber(startWithZero, 1);
			}

			var oxygenRating = Convert.ToInt32(calculateOx, 2);
			var scrubberRating = Convert.ToInt32(calculateScrubber, 2);
			Console.WriteLine($"Oxygen rating: {oxygenRating}\nScrubber rating: {scrubberRating}\nLife support: {oxygenRating * scrubberRating}");
		}

		private static string CalculateScrubber(List<string> lines, int column)
		{         
			while (lines.Count() > 1) {

                var startsWithZeroCount = 0;
                var startsWithOneCount = 0;

                foreach (string line in lines)
                {
                    if (line[column] == '0') startsWithZeroCount++;
                    else startsWithOneCount++;
                }

				if (startsWithZeroCount <= startsWithOneCount) lines = lines.Where(l => l[column] == '0').ToList();
				else lines = lines.Where(l => l[column] == '1').ToList();

                column++;
            } 
			return lines.First();
		}

		private static string CalculateOxygen(List<string> lines, int column)
		{         
			while (lines.Count() > 1) {
				var startsWithZeroCount = 0;
				var startsWithOneCount = 0;
				foreach (string line in lines)
                {
					if (line[column] == '0') startsWithZeroCount++;
					else startsWithOneCount++;
                }
                
				if (startsWithZeroCount > startsWithOneCount) lines = lines.Where(l => l[column] == '0').ToList();
				else lines = lines.Where(l => l[column] == '1').ToList();

				column++;
			}
			return lines.First();
		}
	}
}
