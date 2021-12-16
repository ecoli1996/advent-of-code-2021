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
			var syntaxErrorScore = 0;

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

				foreach (char bracket in line)
				{
					if (bracket == ')')
					{
						syntaxErrorScore += 3;
						break;
					}
					else if (bracket == ']')
					{
						syntaxErrorScore += 57;
						break;
					}
					else if (bracket == '}')
					{
						syntaxErrorScore += 1197;
						break;
					}
					else if (bracket == '>')
					{
						syntaxErrorScore += 25137;
						break;
					}
				}
			}

			Console.WriteLine($"Syntax error score: {syntaxErrorScore}");
		}
	}
}
