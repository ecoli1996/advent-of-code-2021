using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
            var positions = System.IO.File.ReadAllText(@"C:/aoc_day7.txt").Split(',');
			var positions_int = positions.Select(p => int.Parse(p));

			var crabs = new Dictionary<int, IEnumerable<int>>();
			foreach (string position in positions) {
				crabs.TryAdd(int.Parse(position), positions_int);
			}

			int? minFuel = null;
			int? minFuelKey = null;

			foreach (KeyValuePair<int, IEnumerable<int>> crab in crabs) {
				var currentKey = crab.Key;
				var cost = crab.Value.Select(position => position < currentKey ? currentKey - position : position - currentKey).Sum();

				if (minFuel == null || cost < minFuel) {
					minFuel = cost;
					minFuelKey = currentKey;
				}
			}

			Console.WriteLine($"Min fuel cost is {minFuel} for key {minFuelKey}");
		}
	}
}
