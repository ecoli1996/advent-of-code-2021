using System;
using System.Collections.Generic;

namespace AdventOfCode
{
	class Program
	{
		private static int ROW_COUNT = 0;
        private static int MAX_INDEX = 0;

		static void Main(string[] args)
		{
			var lines = System.IO.File.ReadAllLines(@"C:/aoc_day11.txt");
			ROW_COUNT = lines.Length;
            MAX_INDEX = lines[0].Length - 1;
            
			// coordinate, value
			var octopi = new Dictionary<Tuple<int, int>, int>();
			var flashedOctopi = new Dictionary<Tuple<int, int>, bool>();
			var lineNumber = 1;
			foreach(string line in lines)
			{
				for (int i = 0; i < line.Length; i++)
				{
					octopi.Add(new Tuple<int, int>(i, lineNumber), (int)Char.GetNumericValue(line[i]));
				}
				lineNumber++;
			}

			var x = 0;
			var y = 1;
			var isSynchronized = false;
			var currentStep = 1;

			while (!isSynchronized) {
				for (int i = 0; i < octopi.Count; i++)
                {
                    var currentCoordinate = new Tuple<int, int>(x, y);
					if (!flashedOctopi.ContainsKey(currentCoordinate)) {
                        var newValue = octopi[currentCoordinate] + 1;
                        octopi[currentCoordinate] = newValue > 9 ? 0 : newValue;

                        if (newValue > 9)
                        {
                            flashedOctopi.Add(currentCoordinate, true);
                            IncreaseAdjacentOctopi(currentCoordinate, octopi, flashedOctopi);
                        }
					}

					x++;

					if (x > MAX_INDEX)
					{
						x = 0;
						y++;
					}

					if (flashedOctopi.Count == octopi.Count) {
						isSynchronized = true;
						Console.WriteLine($"Synchronized at step: {currentStep}");
						break;
					}
                }

				currentStep++;

				x = 0;
				y = 1;
				flashedOctopi = new Dictionary<Tuple<int, int>, bool>();
			}
		}

		private static int IncreaseAdjacentOctopi(Tuple<int, int> currentCoordinate, Dictionary<Tuple<int, int>, int> octopi, Dictionary<Tuple<int, int>, bool> flashedOctopi)
		{
			var flashCount = 0;
			var adjacentRight = GetAdjacentRight(currentCoordinate);
			var adjacentLeft = GetAdjacentLeft(currentCoordinate);
			var adjacentNorth = GetAdjacentNorth(currentCoordinate);
            var adjacentSouth = GetAdjacentSouth(currentCoordinate);
			var adjacentNorthEast = GetAdjacentNorthEast(adjacentRight, adjacentNorth);
			var adjacentNorthWest = GetAdjacentNorthWest(adjacentLeft, adjacentNorth);
			var adjacentSouthEast = GetAdjacentSouthEast(adjacentRight, adjacentSouth);
			var adjacentSouthWest = GetAdjacentSouthWest(adjacentLeft, adjacentSouth);

			int? updatedRightValue = null;
			if (adjacentRight != null) {
				updatedRightValue = TryUpdateValue(adjacentRight, octopi, flashedOctopi);
				if (updatedRightValue == 0)
				{
					flashCount++;
					flashedOctopi.Add(adjacentRight, true);
				}
			}

			int? updatedLeftValue = null;
            if (adjacentLeft != null)
            {
				updatedLeftValue = TryUpdateValue(adjacentLeft, octopi, flashedOctopi);
				if (updatedLeftValue == 0)
				{
					flashCount++;
					flashedOctopi.Add(adjacentLeft, true);
				}
            }

			int? updatedNorthValue = null;
            if (adjacentNorth != null)
            {
				updatedNorthValue = TryUpdateValue(adjacentNorth, octopi, flashedOctopi);
				if (updatedNorthValue == 0)
				{
					flashCount++;               
					flashedOctopi.Add(adjacentNorth, true);
				}
            }

			int? updatedNorthEastValue = null;
            if (adjacentNorthEast != null)
            {
				updatedNorthEastValue = TryUpdateValue(adjacentNorthEast, octopi, flashedOctopi);
				if (updatedNorthEastValue == 0)
				{
					flashCount++;
					flashedOctopi.Add(adjacentNorthEast, true);
				}
            }

			int? updatedNorthWestValue = null;
            if (adjacentNorthWest != null)
            {
				updatedNorthWestValue = TryUpdateValue(adjacentNorthWest, octopi, flashedOctopi);
				if (updatedNorthWestValue == 0)
				{
					flashCount++;
					flashedOctopi.Add(adjacentNorthWest, true);
				}
            }

			int? updatedSouthValue = null;
            if (adjacentSouth != null)
            {
				updatedSouthValue = TryUpdateValue(adjacentSouth, octopi, flashedOctopi);
				if (updatedSouthValue == 0)
				{
					flashCount++;
					flashedOctopi.Add(adjacentSouth, true);
				}
            }

			int? updatedSouthEastValue = null;
            if (adjacentSouthEast != null)
            {
				updatedSouthEastValue = TryUpdateValue(adjacentSouthEast, octopi, flashedOctopi);
				if (updatedSouthEastValue == 0)
				{
					flashCount++;
					flashedOctopi.Add(adjacentSouthEast, true);
				}
            }

			int? updatedSouthWestValue = null;
            if (adjacentSouthWest != null)
            {
				updatedSouthWestValue = TryUpdateValue(adjacentSouthWest, octopi, flashedOctopi);
				if (updatedSouthWestValue == 0)
				{
					flashCount++;
					flashedOctopi.Add(adjacentSouthWest, true);
				}
            }

			if (updatedRightValue == 0) flashCount += IncreaseAdjacentOctopi(adjacentRight, octopi, flashedOctopi);
			if (updatedLeftValue == 0) flashCount += IncreaseAdjacentOctopi(adjacentLeft, octopi, flashedOctopi);
			if (updatedNorthValue == 0) flashCount += IncreaseAdjacentOctopi(adjacentNorth, octopi, flashedOctopi);
			if (updatedNorthEastValue == 0) flashCount += IncreaseAdjacentOctopi(adjacentNorthEast, octopi, flashedOctopi);
			if (updatedNorthWestValue == 0) flashCount += IncreaseAdjacentOctopi(adjacentNorthWest, octopi, flashedOctopi);
			if (updatedSouthValue == 0) flashCount += IncreaseAdjacentOctopi(adjacentSouth, octopi, flashedOctopi);
			if (updatedSouthEastValue == 0) flashCount += IncreaseAdjacentOctopi(adjacentSouthEast, octopi, flashedOctopi);
			if (updatedSouthWestValue == 0) flashCount += IncreaseAdjacentOctopi(adjacentSouthWest, octopi, flashedOctopi);

			return flashCount;
		}

		private static int? TryUpdateValue(Tuple<int, int> coordinate, Dictionary<Tuple<int, int>, int> octopi, Dictionary<Tuple<int, int>, bool> flashedOctopi)
		{
			if (flashedOctopi.ContainsKey(coordinate)) return null;
			var currentVal = octopi[coordinate];         
			var nextVal = currentVal + 1;
			octopi[coordinate] = nextVal > 9 ? 0 : nextVal;
			return octopi[coordinate];
		}

		private static Tuple<int, int> GetAdjacentSouthWest(Tuple<int, int> adjacentLeft, Tuple<int, int> adjacentSouth)
		{
			if (adjacentLeft == null || adjacentSouth == null) return null;
			return new Tuple<int, int>(adjacentLeft.Item1, adjacentSouth.Item2);
		}

		private static Tuple<int, int> GetAdjacentSouthEast(Tuple<int, int> adjacentRight, Tuple<int, int> adjacentSouth)
		{
			if (adjacentRight == null || adjacentSouth == null) return null;
			return new Tuple<int, int>(adjacentRight.Item1, adjacentSouth.Item2);
		}

		private static Tuple<int, int> GetAdjacentNorthWest(Tuple<int, int> adjacentLeft, Tuple<int, int> adjacentNorth)
		{
			if (adjacentLeft == null || adjacentNorth == null) return null;
			return new Tuple<int, int>(adjacentLeft.Item1, adjacentNorth.Item2);
		}

		private static Tuple<int, int> GetAdjacentNorthEast(Tuple<int, int> adjacentRight, Tuple<int, int> adjacentNorth)
		{
			if (adjacentRight == null || adjacentNorth == null) return null;
			return new Tuple<int, int>(adjacentRight.Item1, adjacentNorth.Item2);
		}

		private static Tuple <int, int>  GetAdjacentSouth(Tuple<int, int> currentCoordinate)
		{
			var nextSouthY = currentCoordinate.Item2 + 1;
			if (nextSouthY <= ROW_COUNT) return new Tuple<int, int>(currentCoordinate.Item1, nextSouthY);
            return null;
		}

		private static Tuple<int, int> GetAdjacentNorth(Tuple<int, int> currentCoordinate)
		{
			var nextNorthY = currentCoordinate.Item2 - 1;
			if (nextNorthY >= 1) return new Tuple<int, int>(currentCoordinate.Item1, nextNorthY);
            return null;
		}

		private static Tuple<int, int> GetAdjacentLeft(Tuple<int, int> currentCoordinate)
		{
			var nextLeftX = currentCoordinate.Item1 - 1;
			if (nextLeftX >= 0) return new Tuple<int, int>(nextLeftX, currentCoordinate.Item2);
            return null;
		}

		private static Tuple<int, int> GetAdjacentRight(Tuple<int, int> currentCoordinate)
		{
			var nextRightX = currentCoordinate.Item1 + 1;
			if (nextRightX <= MAX_INDEX) return new Tuple<int, int>(nextRightX, currentCoordinate.Item2);
			return null;
		}
	}
}
