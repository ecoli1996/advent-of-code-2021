using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"C:\aoc_day14.txt");
            var template = lines[0];
            var possibleCharacters = new HashSet<char>();

            var pairInsertionRules = new Dictionary<string, char>();

            for (int i = 2; i < lines.Length; i++)
            {
                var rule = lines[i].Split("->", StringSplitOptions.None);
                var insertion = rule[1].Trim()[0];
                pairInsertionRules.Add(rule[0].Trim(), insertion);

                if (!possibleCharacters.Contains(insertion)) possibleCharacters.Add(insertion);
            }

            var step = 1;
            while (step <= 10)
            {
                var newTemplate = new StringBuilder();
                var previouslyInserted = false;
                for (int i = 0; i < template.Length - 1; i++)
                {
                    var checkCouple = template.Substring(i, 2);
                    if (pairInsertionRules.TryGetValue(checkCouple, out char insert))
                    {
                        if (previouslyInserted) newTemplate.Append($"{insert}{checkCouple[1]}");
                        else newTemplate.Append($"{checkCouple[0]}{insert}{checkCouple[1]}");
                        previouslyInserted = true;
                    }
                    else
                    {
                        newTemplate.Append(checkCouple[0]);
                        previouslyInserted = false;
                    }
                }
                template = newTemplate.ToString();
                step++;
            }

            int minCount = int.MaxValue;
            var maxCount = 0;

            foreach (char character in possibleCharacters)
            {
                var count = template.Count(c => c == character);
                if (count < minCount) minCount = count;
                if (count > maxCount) maxCount = count;
            }

            Console.WriteLine(maxCount - minCount);
        }
    }
}
