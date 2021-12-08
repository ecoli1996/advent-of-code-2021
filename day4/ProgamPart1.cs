using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{
		static void Main(string[] args)
		{
			var lines = System.IO.File.ReadAllLines(@"C:/aoc_day4.txt");
            
			var calloutOrder = lines[0].Split(',');

			// key = player, value = { row #, row }
			var allPlayersBoards_Rows = BuildAllPlayersRows(lines);

			// key = player, value = { column #, column }
			var allPlayersBoards_Columns = BuildAllPlayersColumns(lines);

			var winningSum = 0;
			var winningCalloutNumber = 0;
			foreach (string number in calloutOrder) {
				winningSum = RemoveNumberFromRows(number, allPlayersBoards_Rows);

				if (winningSum != 0)
				{
					winningCalloutNumber = int.Parse(number);
					break;
				}

				winningSum = RemoveNumberFromColumns(number, allPlayersBoards_Columns);

				if (winningSum != 0)
				{
					winningCalloutNumber = int.Parse(number);
					break;
				}
			}

			Console.WriteLine($"The winning sum is: {winningSum}");
			Console.WriteLine($"The winning callout number is: {winningCalloutNumber}");
			Console.WriteLine($"The winning product is: {winningSum * winningCalloutNumber}");         
		}

		private static int RemoveNumberFromColumns(string numberToRemove, Dictionary<int, Dictionary<int, List<string>>> allPlayersBoards_Columns)
		{
			var winningPlayer = false;
			var numberOfPlayers = allPlayersBoards_Columns.Count;
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                var currentRow = 1;
				        var playerValues = allPlayersBoards_Columns[i];
                var numberOfValues = playerValues.Count;
                for (int j = 1; j <= numberOfValues; j++)
                {
					        playerValues[currentRow] = playerValues[currentRow].Where(n => n != numberToRemove).ToList();
					        if (playerValues[currentRow].Count == 0)
                    {
                        winningPlayer = true;
                        break;
                    }
                    currentRow++;
                }

                if (winningPlayer) return CalculateSumOfBoard(playerValues);
            }
            return 0;
		}
      
		private static int RemoveNumberFromRows(string numberToRemove, Dictionary<int, Dictionary<int, string[]>> allPlayersBoards_Rows)
		{
			var winningPlayer = false;
			var numberOfPlayers = allPlayersBoards_Rows.Count;
			for (int i = 1; i <= numberOfPlayers; i++)
            {
                var currentRow = 1;
				        var playerValues = allPlayersBoards_Rows[i];
				        var numberOfValues = playerValues.Count;
				        for (int j = 1; j <= numberOfValues; j++)
                {
                  playerValues[currentRow] = playerValues[currentRow].Where(n => n != numberToRemove && !string.IsNullOrWhiteSpace(n)).ToArray();
                  if (playerValues[currentRow].Length == 0)
                  {
                    winningPlayer = true;
                    break;
                  }
                  currentRow++;
                }

                if (winningPlayer) return CalculateSumOfBoard(playerValues);
            }
            return 0;
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
