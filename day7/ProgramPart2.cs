using System;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
            var positions = System.IO.File.ReadAllText(@"C:/aoc_day7.txt").Split(',');
			var positions_int = positions.Select(p => int.Parse(p));

			var maxKey = 0;
			foreach (int position in positions_int) {
				if (position > maxKey) maxKey = position;
			}

			int? minFuel = null;
			int? minFuelKey = null;

			for (int positionCandidate = 0; positionCandidate <= maxKey; positionCandidate++) {
				var cost = positions_int.Select((crabPosition) => {
					var diff = crabPosition - positionCandidate;

					if (diff == 0) return diff;
					if (diff < 0) diff *= -1;

					return Enumerable.Range(1, diff).Sum();
				}).Sum();

				if (minFuel == null || cost < minFuel) {
					minFuel = cost;
					minFuelKey = positionCandidate;
				}
			}

			Console.WriteLine($"Min fuel cost is {minFuel} for position {minFuelKey}");
		}
	}
}
