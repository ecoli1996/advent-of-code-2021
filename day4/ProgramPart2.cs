using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
			var lines = System.IO.File.ReadAllLines(@"/Users/evc1996/Projects/AdventOfCode/AdventOfCode/aoc_day4.txt");
            
			var calloutOrder = lines[0].Split(',');

			// key = player, value = { row #, row }
			var allPlayersBoards_Rows = BuildAllPlayersRows(lines);

			// key = player, value = { column #, column }
			var allPlayersBoards_Columns = BuildAllPlayersColumns(lines);

			var lastCalloutNumber = 0;
			var losingSum = 0;
			var numberOfPlayers = allPlayersBoards_Rows.Count;
			var winningPlayersRows = new SortedDictionary<DateTimeOffset, Dictionary<int, string[]>>();
			var winningPlayersColumns = new SortedDictionary<DateTimeOffset, Dictionary<int, List<string>>>();

			foreach (string number in calloutOrder) {
				RemoveNumberFromRows(number, allPlayersBoards_Rows, winningPlayersRows, allPlayersBoards_Columns, numberOfPlayers);

				if (winningPlayersRows.Count + winningPlayersColumns.Count == numberOfPlayers)
				{
					lastCalloutNumber = int.Parse(number);
					losingSum = CalculateSumOfBoard(winningPlayersRows.Last().Value);
					break;
				}

				RemoveNumberFromColumns(number, allPlayersBoards_Columns, winningPlayersColumns, allPlayersBoards_Rows, numberOfPlayers);

				if (winningPlayersRows.Count + winningPlayersColumns.Count == numberOfPlayers)
				{
					lastCalloutNumber = int.Parse(number);
					losingSum = CalculateSumOfBoard(winningPlayersColumns.Last().Value);
					break;
				}
			}

			Console.WriteLine($"The losing sum is: {losingSum}");
			Console.WriteLine($"The last callout number is: {lastCalloutNumber}");
			Console.WriteLine($"The losing product is: {losingSum * lastCalloutNumber}");         
		}

		private static void RemoveNumberFromColumns(string numberToRemove, Dictionary<int, Dictionary<int, List<string>>> allPlayersBoards_Columns, SortedDictionary<DateTimeOffset, Dictionary<int, List<string>>> winningPlayers, Dictionary<int, Dictionary<int, string[]>> allPlayersBoards_Rows, int numberOfPlayers)
		{
            for (int i = 1; i <= numberOfPlayers; i++)
            {
				if (allPlayersBoards_Columns.ContainsKey(i))
				{
					var playerValues = allPlayersBoards_Columns[i];
					var numberOfValues = playerValues.Count;
					for (int j = 1; j <= numberOfValues; j++)
					{
						playerValues[j] = playerValues[j].Where(n => n != numberToRemove && !string.IsNullOrWhiteSpace(n)).ToList();
						if (playerValues[j].Count == 0)
						{
							winningPlayers.Add(DateTimeOffset.Now, playerValues);
							allPlayersBoards_Columns.Remove(i);
							allPlayersBoards_Rows.Remove(i);
							break;
						}
					}
				}
            }
		}
      
		private static void RemoveNumberFromRows(string numberToRemove, Dictionary<int, Dictionary<int, string[]>> allPlayersBoards_Rows, SortedDictionary<DateTimeOffset, Dictionary<int, string[]>> winningPlayers, Dictionary<int, Dictionary<int, List<string>>> allPlayersBoards_Columns, int numberOfPlayers)
		{
			for (int i = 1; i <= numberOfPlayers; i++)
            {
				if (allPlayersBoards_Rows.ContainsKey(i)) {
                    var playerValues = allPlayersBoards_Rows[i];
                    var numberOfValues = playerValues.Count;
                    for (int j = 1; j <= numberOfValues; j++)
                    {
                        playerValues[j] = playerValues[j].Where(n => n != numberToRemove && !string.IsNullOrWhiteSpace(n)).ToArray();
                        if (playerValues[j].Length == 0)
                        {
							winningPlayers.Add(DateTimeOffset.Now, playerValues);
							allPlayersBoards_Rows.Remove(i);
							allPlayersBoards_Columns.Remove(i);
                            break;
                        }
                    }
				}
            }
		}

		private static int CalculateSumOfBoard(Dictionary<int, List<string>> board)
        {
            var sum = 0;
			foreach (List<string> setOfValues in board.Values)
            {
                sum += setOfValues.Select(val => int.Parse(val)).Sum();
            }
            return sum;
        }

		private static int CalculateSumOfBoard(Dictionary<int, string[]> board)
        {
            var sum = 0;
			foreach (string[] setOfValues in board.Values)
            {
                sum += setOfValues.Select(val => int.Parse(val)).Sum();
            }
            return sum;
        }

		private static Dictionary<int, Dictionary<int, string[]>> BuildAllPlayersRows(string[] lines)
		{
			var allPlayersBoards_Rows = new Dictionary<int, Dictionary<int, string[]>>();
            var currentPlayersBoard = new Dictionary<int, string[]>();
            var rowNumber = 1;
            var playerNumber = 1;

            for (int i = 2; i < lines.Length; i++)
            {
                var line = lines[i];

                // if line is empty we have begun a new players board
                if (string.IsNullOrWhiteSpace(line))
                {
                    allPlayersBoards_Rows.Add(playerNumber, currentPlayersBoard);
                    currentPlayersBoard = new Dictionary<int, string[]>();
                    rowNumber = 1;
                    playerNumber++;
                }
                else
                {
                    currentPlayersBoard.Add(rowNumber, line.Split());
                    rowNumber++;
                }
            }

			allPlayersBoards_Rows.Add(playerNumber, currentPlayersBoard);
			return allPlayersBoards_Rows;
		}

		private static Dictionary<int, Dictionary<int, List<string>>> BuildAllPlayersColumns(string[] lines)
        {
            var allPlayersBoards_Columns = new Dictionary<int, Dictionary<int, List<string>>>();
            var currentPlayersBoard = new Dictionary<int, List<string>>();
            var playerNumber = 1;

            for (int i = 2; i < lines.Length; i++)
            {
				var line = lines[i];
                var columnNumber = 1;

                // if line is empty we have begun a new players board
                if (string.IsNullOrWhiteSpace(line))
                {
					allPlayersBoards_Columns.Add(playerNumber, currentPlayersBoard);
                    currentPlayersBoard = new Dictionary<int, List<string>>();
                    playerNumber++;
                }
                else
                {
					var row = line.Split();
					foreach (string number in row) {
						if (!string.IsNullOrEmpty(number)) {
                            if (currentPlayersBoard.ContainsKey(columnNumber)) currentPlayersBoard[columnNumber].Add(number);
                            else currentPlayersBoard.Add(columnNumber, new List<string> { number });
                            columnNumber++;
						}
					}
                }
            }

			allPlayersBoards_Columns.Add(playerNumber, currentPlayersBoard);
			return allPlayersBoards_Columns;
        }
	}
}
