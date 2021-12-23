using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"C:\aoc_day14.txt");
            var template = lines[0].Trim();
            var countOfPairs = new Dictionary<string, ulong>();
            var characterCounts = new Dictionary<char, ulong>();

            for (int i = 0; i < template.Length - 1; i++)
            {
                var templateToAdd = template.Substring(i, 2);
                if (countOfPairs.ContainsKey(templateToAdd)) countOfPairs[templateToAdd] = countOfPairs[templateToAdd] + 1;
                else countOfPairs.Add(templateToAdd, 1);

                if (i == 0)
                {
                    var firstChar = templateToAdd[0];
                    if (characterCounts.ContainsKey(firstChar)) characterCounts[firstChar] = characterCounts[firstChar] + 1;
                    else characterCounts.Add(firstChar, 1);
                }
                var secondChar = templateToAdd[1];
                if (characterCounts.ContainsKey(secondChar)) characterCounts[secondChar] = characterCounts[secondChar] + 1;
                else characterCounts.Add(secondChar, 1);
            }

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
            while (step <= 40)
            {
                var newCountOfPairs = new Dictionary<string, ulong>();
                foreach (KeyValuePair<string, ulong> pair in countOfPairs)
                {
                    if (pairInsertionRules.ContainsKey(pair.Key))
                    {
                        var charToInsert = pairInsertionRules[pair.Key];
                        var countOfRule = pair.Value;
                        var newPair1 = $"{pair.Key[0]}{charToInsert}";
                        var newPair2 = $"{charToInsert}{pair.Key[1]}";

                        if (newCountOfPairs.ContainsKey(newPair1)) newCountOfPairs[newPair1] = newCountOfPairs[newPair1] + countOfRule;
                        else newCountOfPairs.Add(newPair1, countOfRule);
                        if (newCountOfPairs.ContainsKey(newPair2)) newCountOfPairs[newPair2] = newCountOfPairs[newPair2] + countOfRule;
                        else newCountOfPairs.Add(newPair2, countOfRule);

                        if (characterCounts.ContainsKey(charToInsert)) characterCounts[charToInsert] = characterCounts[charToInsert] + countOfRule;
                        else characterCounts.Add(charToInsert, countOfRule);
                    }
                    else
                    {
                        newCountOfPairs.Add(pair.Key, pair.Value);
                    }
                }
                countOfPairs = newCountOfPairs;
                step++;
            }

            var maxCount = characterCounts.Max(x => x.Value);
            var minCount = characterCounts.Min(x => x.Value);
            Console.WriteLine(maxCount - minCount);
        }
    }
}
