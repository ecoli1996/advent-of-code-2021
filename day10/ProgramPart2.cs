using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
	class Program
	{      
		static void Main(string[] args)
		{
			var lines = System.IO.File.ReadAllLines(@"C:/aoc_day10.txt");
			var allScores = new SortedList<ulong, int>();

			for (int i = 0; i < lines.Length; i++)
			{
				var keepSearching = true;
				var line = lines[i];

				while (keepSearching) {
					keepSearching = false;
					for (int bracketIndex = 0; bracketIndex < line.Length - 1; bracketIndex++)
					{
					    	var char1 = line[bracketIndex];
						var char2 = line[bracketIndex + 1];
            
					    	if ((char1 == '(' && char2 == ')') ||
							(char1 == '{' && char2 == '}') ||
							(char1 == '[' && char2 == ']') ||
							(char1 == '<' && char2 == '>'))
						{
						  	line = line.Remove(bracketIndex, 2);
							keepSearching = true;
							break;
                				}
          				}
				}

				ulong autocompleteScore = 0;
				var syntaxErrorFound = line.IndexOfAny(new char[] { ')', ']', '}', '>' }) != -1;

				if (!syntaxErrorFound) {
					for (int bracketIndex = line.Length - 1; bracketIndex >= 0; bracketIndex--)
				  	{
						if (line[bracketIndex] == '(')
				      		{
							autocompleteScore *= 5;
							autocompleteScore += 1;
				      		}
					      	else if (line[bracketIndex] == '[')
					      	{
							autocompleteScore *= 5;
							autocompleteScore += 2;
					      	}
				      		else if (line[bracketIndex] == '{')
				      		{
							autocompleteScore *= 5;
							autocompleteScore += 3;
				      		}
				      		else if (line[bracketIndex] == '<')
				      		{
							autocompleteScore *= 5;
							autocompleteScore += 4;
				      		}
				  	}
					
          				Console.WriteLine($"Autocomplete score: {autocompleteScore}");
					allScores.Add(autocompleteScore, allScores.Count);
				}
			}

			var middleScore = (int)Math.Ceiling((double)(allScores.Count / 2));
			Console.WriteLine($"Middle score: {allScores.Keys.ElementAt(middleScore)}");
		}
	}
}
