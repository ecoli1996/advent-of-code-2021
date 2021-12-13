using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
			// # - chars - unique
			// 0 - 6 { a b c e f g}
			// 1 - 2 { c f }  - *
			// 2 - 5 { a c d f g }
			// 3 - 4 { b c d f } - * 
			// 5 - 5 { a b d f g }
			// 6 - 6 { a b d e f g }
			// 7 - 3 { a c f } - * 
			// 8 - 7 { a b c d e f g } - *
			// 9 - 6 { a b c d f g }
			var wiringConfigs = System.IO.File.ReadAllLines(@"C:/aoc_day8.txt");
			var numberOfUniqueDigits = 0;
			var wiringOutputs = new Dictionary<int, string[]>();

			for (int i = 0; i < wiringConfigs.Length; i++) {
				var splitLine = wiringConfigs[i].Split('|');
				var output = splitLine[1].Split(' ');
				wiringOutputs.Add(i+1, output);
			}

			foreach (KeyValuePair<int, string[]> output in wiringOutputs) {
				numberOfUniqueDigits += output.Value.Where(chars => chars.Length == 2 || chars.Length == 4 || chars.Length == 3 || chars.Length == 7).Count();
			}

			Console.WriteLine($"Num of unique segments: {numberOfUniqueDigits}");
		}
	}
}
