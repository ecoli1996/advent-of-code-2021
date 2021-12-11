using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
			// fish #, timer
            var startTimers = System.IO.File.ReadAllText(@"C:/aoc_day6.txt").Split(',');
            // timer, # of fish
			var fishies = new Dictionary<int, ulong> {
				{0, 0},
				{1, 0},
				{2, 0},
				{3, 0},
				{4, 0},
				{5, 0},
				{6, 0},
				{7, 0},
				{8, 0}
			};

			foreach(string startTimer in startTimers) {
				var t = int.Parse(startTimer);
				fishies[t] = fishies[t] + 1;
			}

			var totalDays = 256;
			for (int day = 1; day <= totalDays; day++) {
				var negativeOnes = fishies[0];
				var zeros = fishies[1];
				var ones = fishies[2];
				var twos = fishies[3];
				var threes = fishies[4];
				var fours = fishies[5];
				var fives = fishies[6];
				var sixes = fishies[7];
				var sevens = fishies[8];

				fishies[0] = zeros;
				fishies[1] = ones;
				fishies[2] = twos;
				fishies[3] = threes;
				fishies[4] = fours;
				fishies[5] = fives;
				fishies[6] = sixes + negativeOnes;
				fishies[7] = sevens;
				fishies[8] = negativeOnes;
			}

			ulong totalFish = 0;
			foreach (ulong fishCount in fishies.Values) {
				totalFish += fishCount;
			}
			Console.WriteLine($"Fish count after {totalDays} days: {totalFish}");
		}
	}
}
