using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{         
			var lines = System.IO.File.ReadAllLines(@"C:/aoc_day5.txt");
            // coord, number of intersections
			var intersectingCoordinates = new Dictionary<Tuple<int, int>, int>();
         
			foreach (string line in lines) {
				var coordinates = line.Split(new[] { "->" }, StringSplitOptions.None);
				var firstPair = coordinates[0].Split(',');
				var secondPair = coordinates[1].Split(',');

				var y2 = int.Parse(secondPair[1]);
				var y1 = int.Parse(firstPair[1]);
				var x2 = int.Parse(secondPair[0]);
				var x1 = int.Parse(firstPair[0]);
                
                var minXValue = x1 < x2 ? x1 : x2;
				var maxXValue = x1 > x2 ? x1 : x2;
                var minYValue = y1 < y2 ? y1 : y2;
                var maxYValue = y1 > y2 ? y1 : y2;

				if (x2 - x1 == 0) {
					// line is vertical
					var currentXValue = x1;

					for (int currentYValue = minYValue; currentYValue <= maxYValue; currentYValue++) {
						var currentCoordinate = new Tuple<int, int>(currentXValue, currentYValue);

						if (intersectingCoordinates.ContainsKey(currentCoordinate)) intersectingCoordinates[currentCoordinate] = intersectingCoordinates[currentCoordinate] + 1;
						else intersectingCoordinates.Add(currentCoordinate, 0);
					}
				}
				else {
					var m = (y2 - y1) / (x2 - x1);
					if (m == 0)
                    {
                        // line is horizontal
                        var currentYValue = y1;

                        for (int currentXValue = minXValue; currentXValue <= maxXValue; currentXValue++)
                        {
                            var currentCoordinate = new Tuple<int, int>(currentXValue, currentYValue);

                            if (intersectingCoordinates.ContainsKey(currentCoordinate)) intersectingCoordinates[currentCoordinate] = intersectingCoordinates[currentCoordinate] + 1;
                            else intersectingCoordinates.Add(currentCoordinate, 0);
                        }
					} else {
						// y = mx + b
						// y - b = mx
						// -b = mx - y
						// b = (mx - y) / -1
						var b = (m * x1 - y1) / -1;
						for (int currentXValue = minXValue; currentXValue <= maxXValue; currentXValue++) {
							var currentYValue = m * currentXValue + b;
							var currentCoordinate = new Tuple<int, int>(currentXValue, currentYValue);

                            if (intersectingCoordinates.ContainsKey(currentCoordinate)) intersectingCoordinates[currentCoordinate] = intersectingCoordinates[currentCoordinate] + 1;
                            else intersectingCoordinates.Add(currentCoordinate, 0);
						}
					}
				}
			}

			var coordinatesWithIntersectionsCount = intersectingCoordinates.Values.Where(v => v > 0).Count();
			Console.WriteLine($"Num of intersections: {coordinatesWithIntersectionsCount}");
		}
	}
}
