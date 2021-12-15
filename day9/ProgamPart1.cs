using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	class Program
	{
		private static int ROW_COUNT = 0;
		private static int MAX_INDEX = 0;

		static void Main(string[] args)
		{
			var lines = System.IO.File.ReadAllLines(@"C:/aoc_day9.txt");
            
            		// line #, list of smoke flow heights in that position
			var heightMap = new Dictionary<int, char[]>();
			var lineNumber = 1;
			foreach (string line in lines)
			{
				heightMap.Add(lineNumber, line.ToCharArray());
				lineNumber++;
			}

			ROW_COUNT = heightMap.Count;
			MAX_INDEX = heightMap[1].Length - 1;

			var lowestPoints = new List<double>();

			foreach (KeyValuePair<int, char[]> heights in heightMap)
			{
				var heightsValue = heights.Value;
                		var index = 0;
				foreach(char height in heightsValue)
				{
					var currentHeight = Char.GetNumericValue(height);
					var adjacentHeight_right = GetAdjacentRight(heightsValue, index);
					var adjacentHeight_left = GetAdjacentLeft(heightsValue, index);
					var adjacentHeight_north = GetAdjacentNorth(heightMap, heights.Key, index);
					var adjacentHeight_south = GetAdjacentSouth(heightMap, heights.Key, index);

					if ((adjacentHeight_right == null || currentHeight < adjacentHeight_right) &&
					    (adjacentHeight_left == null || currentHeight < adjacentHeight_left) &&
					    (adjacentHeight_north == null || currentHeight < adjacentHeight_north) &&
					    (adjacentHeight_south == null || currentHeight < adjacentHeight_south)) {
						lowestPoints.Add(currentHeight);
					}
                    			index++;
				}
			}

			var riskLevel = lowestPoints.Sum() + lowestPoints.Count();
			Console.WriteLine($"Risk level is {riskLevel}");
		}

		private static double? GetAdjacentRight(char[] heights, int index)
		{
			if (index < MAX_INDEX) return Char.GetNumericValue(heights[index + 1]);
			return null;
		}

		private static double? GetAdjacentLeft(char[] heights, int index)
        	{
			if (index > 0) return Char.GetNumericValue(heights[index - 1]);
            		return null;
        	}

		private static double? GetAdjacentNorth(Dictionary<int, char[]> heightMap, int currentKey, int index)
        	{
			if (currentKey > 1) return Char.GetNumericValue(heightMap[currentKey - 1][index]);
            		return null;
        	}

		private static double? GetAdjacentSouth(Dictionary<int, char[]> heightMap, int currentKey, int index)
        	{
			if (currentKey < ROW_COUNT) return Char.GetNumericValue(heightMap[currentKey + 1][index]);
			return null;
        	}
	}
}
