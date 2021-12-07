using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
	{
		static void Main(string[] args)
		{
			// placement, count
			var oneBitCounts = new Dictionary<int, int>();
			var zeroBitCounts = new Dictionary<int, int>();

			var lines = System.IO.File.ReadAllLines(@"C:/aoc_day3.txt");
			var lineLength = lines[0].Length;

			for (int i = 1; i <= lineLength; i++) {
				oneBitCounts.Add(i, 0);
				zeroBitCounts.Add(i, 0);
			}         

			var column = 1;
			foreach (string line in lines)
			{
				var bits = line.ToCharArray();
				foreach (char bit in bits)
				{
					if (bit == '0') zeroBitCounts[column] = zeroBitCounts[column] + 1;
					else oneBitCounts[column] = oneBitCounts[column] + 1;

					column++;
					if (column > lineLength) column = 1;
				}
			}

			var gammaRate = "";
			var epsilonRate = "";

			for (int i = 1; i <= lineLength; i++) {
				if (oneBitCounts[i] > zeroBitCounts[i]) {
					gammaRate += "1";
					epsilonRate += "0";
				}
				else 
				{
					epsilonRate += "1";
					gammaRate += "0";
				}
      }

			var gammaRateDec = Convert.ToInt32(gammaRate, 2);
			var epsilonRateDec = Convert.ToInt32(epsilonRate, 2);
			Console.WriteLine($"Gamma rate: {gammaRateDec}\nEpsilon rate: {epsilonRateDec}\nPower consumption: {gammaRateDec * epsilonRateDec}");
		}
	}
}
