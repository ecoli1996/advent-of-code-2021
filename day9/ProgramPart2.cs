using System;
using System.Collections.Generic;
using System.Linq;

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
            		// line #, index, mapped
			var basinMap = new Dictionary<int, Dictionary<int, bool>>();
			var lineNumber = 1;
			foreach (string line in lines)
			{
				var digits = line.ToCharArray();
				heightMap.Add(lineNumber, digits);
				basinMap.Add(lineNumber, digits.Select((val, index) => new { Index = index, Value = val }).ToDictionary(i => i.Index, v => false));
				lineNumber++;
			}

			ROW_COUNT = heightMap.Count;
			MAX_INDEX = heightMap[1].Length - 1;

            		// coordinates of low points
			var lowestPoints = new List<Tuple<int, int>>();
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
						lowestPoints.Add(new Tuple<int, int>(index, heights.Key));
					}
                    			index++;
				}
			}

			var top1BasinCount = 0;
			var top2BasinCount = 0;
			var top3BasinCount = 0;

			foreach (Tuple<int, int> lowPoint in lowestPoints) {
				basinMap[lowPoint.Item2][lowPoint.Item1] = true;            
				var basinCount = GetBasinCount(basinMap, heightMap, lowPoint) + 1;

				if (basinCount > top3BasinCount) {
					top3BasinCount = basinCount;

					if (basinCount > top2BasinCount) {
						top3BasinCount = top2BasinCount;
						top2BasinCount = basinCount;

						if (basinCount > top1BasinCount) {
							top2BasinCount = top1BasinCount;
							top1BasinCount = basinCount; 
						}
					}
				}
			}

			var largestBasinProduct = top1BasinCount * top2BasinCount * top3BasinCount;
			Console.WriteLine($"1st: {top1BasinCount}");
			Console.WriteLine($"2nd: {top2BasinCount}");
			Console.WriteLine($"3rd: {top3BasinCount}");
			Console.WriteLine($"Product: {largestBasinProduct}");
		}

		private static int GetBasinCount(Dictionary<int, Dictionary<int, bool>> basinMap, Dictionary<int, char[]> heightMap, Tuple<int, int> coordinate)
		{
			var basinsToTraverse = new List<Tuple<int, int>>();
			var traversalCount = 0;

			var adjRight = GetAdjacentRight(heightMap[coordinate.Item2], coordinate.Item1);
			var nextRight = coordinate.Item1 + 1;
         
            		while (adjRight != 9 && adjRight != null)
            		{
				if (!basinMap[coordinate.Item2][nextRight]) {
					traversalCount++;
					basinMap[coordinate.Item2][nextRight] = true;
					basinsToTraverse.Add(new Tuple<int, int>(nextRight, coordinate.Item2));
				}

				adjRight = GetAdjacentRight(heightMap[coordinate.Item2], nextRight);
                		nextRight++;
            		}

			var adjLeft = GetAdjacentLeft(heightMap[coordinate.Item2], coordinate.Item1);
			var nextLeft = coordinate.Item1 - 1;
            		
			while (adjLeft != 9 && adjLeft != null)
            		{
				if (!basinMap[coordinate.Item2][nextLeft]) {
					traversalCount++;
					basinMap[coordinate.Item2][nextLeft] = true;
					basinsToTraverse.Add(new Tuple<int, int>(nextLeft, coordinate.Item2));
				}

				adjLeft = GetAdjacentLeft(heightMap[coordinate.Item2], nextLeft);
                		nextLeft--;
            		}

			var adjNorth = GetAdjacentNorth(heightMap, coordinate.Item2, coordinate.Item1);
			var nextNorth = coordinate.Item2 - 1;
            		while (adjNorth != 9 && adjNorth != null)
            		{
				if (!basinMap[nextNorth][coordinate.Item1]) {
					traversalCount++;
					basinMap[nextNorth][coordinate.Item1] = true;
					basinsToTraverse.Add(new Tuple<int, int>(coordinate.Item1, nextNorth));
				}

				adjNorth = GetAdjacentNorth(heightMap, nextNorth, coordinate.Item1);
                		nextNorth--;
            		}		

			var adjSouth = GetAdjacentSouth(heightMap, coordinate.Item2, coordinate.Item1);
			var nextSouth = coordinate.Item2 + 1;
			while (adjSouth != 9 && adjSouth != null)
            		{
				if (!basinMap[nextSouth][coordinate.Item1]) {               
					traversalCount++;
					basinMap[nextSouth][coordinate.Item1] = true;
					basinsToTraverse.Add(new Tuple<int, int>(coordinate.Item1, nextSouth));
				}

				adjSouth = GetAdjacentSouth(heightMap, nextSouth, coordinate.Item1);
                		nextSouth++;
            		}

			if (basinsToTraverse.Count == 0) {
				return traversalCount;
			}

			foreach (Tuple<int, int> basinMember in basinsToTraverse) {
				traversalCount += GetBasinCount(basinMap, heightMap, basinMember);
			}

			return traversalCount;
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
