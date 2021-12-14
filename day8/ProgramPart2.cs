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
			// # - char count - unique*
			// 0 - 6 { a b c e f g}
			// 1 - 2 { c f }  - *
			// 2 - 5 { a c d e g }
			// 3 - 5 { a c d f g }
			// 4 - 4 { b c d f } - * 
			// 5 - 5 { a b d f g }
			// 6 - 6 { a b d e f g }
			// 7 - 3 { a c f } - * 
			// 8 - 7 { a b c d e f g } - *
			// 9 - 6 { a b c d f g }
			var wiringConfigs = System.IO.File.ReadAllLines(@"C:/aoc_day8.txt");
			var sum = 0;
			var wiringInputs = new Dictionary<int, string[]>();
			var wiringOutputs = new Dictionary<int, string[]>();

			for (int i = 0; i < wiringConfigs.Length; i++) {
				var splitLine = wiringConfigs[i].Split(new[] { " | "}, StringSplitOptions.None);
				var input = splitLine[0].Split(' ');
				var output = splitLine[1].Split(' ');
				wiringOutputs.Add(i + 1, output);
				wiringInputs.Add(i + 1, input);
			}

			for (int i = 1; i <= wiringInputs.Count; i++) {
				var currentInputs = wiringInputs[i];
				var currentOutputs = wiringOutputs[i];
				var knownConfigurations = new Dictionary<string, char>();
				var knownCharPositions = new Dictionary<Position, char>();

				var eight = FindUniqueLengthElement(currentInputs, 8);
				knownConfigurations.Add(eight, '8');

				var seven = FindUniqueLengthElement(currentInputs, 7);
				knownConfigurations.Add(seven, '7');

				var four = FindUniqueLengthElement(currentInputs, 4);
				knownConfigurations.Add(four, '4');

                var one = FindUniqueLengthElement(currentInputs, 1);
				knownConfigurations.Add(one, '1');

				var two = FindTwo(currentInputs, knownCharPositions, one);
				knownConfigurations.Add(two, '2');

				var (five, six) = FindFiveAndSix(currentInputs, knownCharPositions);
				knownConfigurations.Add(five, '5');
				knownConfigurations.Add(six, '6');

				var three = FindThree(currentInputs, two, five);
				knownConfigurations.Add(three, '3');

				var (zero, nine) = FindZeroAndNine(currentInputs, one, two, three, four, five, six, seven, eight);
				knownConfigurations.Add(zero, '0');
				knownConfigurations.Add(nine, '9');

				sum += GetFullNumberOfCurrentOutputs(currentOutputs, knownConfigurations);
			}

			Console.WriteLine($"Sum of segments: {sum}");
		}

		private static int GetFullNumberOfCurrentOutputs(string[] currentOutputs, Dictionary<string, char> knownConfigurations)
		{
			var stringNum = new StringBuilder();
			foreach(string output in currentOutputs) {
				var possibleOutputs = knownConfigurations.Where(k => k.Key.Length == output.Length);

				if (possibleOutputs.Count() == 1) stringNum.Append(possibleOutputs.First().Value);
				else stringNum.Append(possibleOutputs.First(o => o.Key.Except(output).Count() == 0).Value);
			}

			Console.WriteLine($"Current display: {stringNum.ToString()}");
			return int.Parse(stringNum.ToString());
		}

		private static Tuple<string, string> FindZeroAndNine(string[] currentInputs, string one, string two, string three, string four, string five, string six, string seven, string eight)
		{
			var notZeroOrNine = new[] { one, two, three, four, five, six, seven, eight };
			var zeroOrNine = currentInputs.Except(notZeroOrNine);

			var zeroOrNineChars = zeroOrNine.First();
			var fourExcept = four.Except(zeroOrNineChars);
			if (fourExcept.Count() == 1) {
				return new Tuple<string, string>(zeroOrNine.First(), zeroOrNine.ElementAt(1));
			}
			return new Tuple<string, string>(zeroOrNine.ElementAt(1), zeroOrNine.First());
		}

		private static string FindThree(string[] currentInputs, string two, string five)
		{
			var countOfThree = 5;
			foreach(string input in currentInputs) {
				if (input.Length == countOfThree && !string.Equals(input, two) && !string.Equals(input, five)) {
					return input;
				}
			}
			return string.Empty;
		}

		private static Tuple<string, string> FindFiveAndSix(string[] currentInputs, Dictionary<Position, char> knownCharPositions)
		{
			var countOfFive = 5;
			var countOfSix = 6;
			var rightUp = knownCharPositions[Position.RightUp];
			var foundFive = false;
			var foundSix = false;
			var i = 0;
			var five = string.Empty;
			var six = string.Empty;

			while (!foundFive || !foundSix) {
				var input = currentInputs[i];
				if (!foundFive && input.Length == countOfFive && input.IndexOf(rightUp) == -1)
                {
					foundFive = true;
					five = input;
                }
				if (!foundSix && input.Length == countOfSix && input.IndexOf(rightUp) == -1)
                {
					foundSix = true;
					six = input;
                }
				i++;
			}

			return new Tuple<string, string>(five, six);
		}
      
		private static string FindTwo(string[] currentInputs, Dictionary<Position, char> knownCharPositions, string one)
		{
			var aIn = 0;
			var bIn = 0;
			var cIn = 0;
			var dIn = 0;
			var eIn = 0;
			var fIn = 0;
			var gIn = 0;

			var notInA = new Dictionary<string, bool>();
			var notInB = new Dictionary<string, bool>();
			var notInC = new Dictionary<string, bool>();
			var notInD = new Dictionary<string, bool>();
			var notInE = new Dictionary<string, bool>();
			var notInF = new Dictionary<string, bool>();
			var notInG = new Dictionary<string, bool>();         

			foreach (string input in currentInputs) {
				var chars = input.ToCharArray();

                if (chars.Contains('a')) aIn += 1;
                else notInA.Add(input, true);

                if (chars.Contains('b')) bIn += 1;
				else notInB.Add(input, true);

                if (chars.Contains('c')) cIn += 1;
                else notInC.Add(input, true);

                if (chars.Contains('d')) dIn += 1;
                else notInD.Add(input, true);

                if (chars.Contains('e')) eIn += 1;
                else notInE.Add(input, true);

                if (chars.Contains('f')) fIn += 1;
                else notInF.Add(input, true);

                if (chars.Contains('g')) gIn += 1;
                else notInG.Add(input, true);           
			}
         
			if (aIn == 9)
			{            
				var oneExceptA = one.Except(new[] { 'a' }).Single();
				knownCharPositions.Add(Position.RightUp, oneExceptA);

				return notInA.Single().Key;           
			}
			else if (bIn == 9)
			{            
                var oneExceptB = one.Except(new[] { 'b' }).Single();
				knownCharPositions.Add(Position.RightUp, oneExceptB);

				return notInB.Single().Key;
                 
			}
			else if (cIn == 9)
			{
				var oneExceptC = one.Except(new[] { 'c' }).Single();
				knownCharPositions.Add(Position.RightUp, oneExceptC);

				return notInC.Single().Key;
			}
			else if (dIn == 9)
			{            
                var oneExceptD = one.Except(new[] { 'd' }).Single();
				knownCharPositions.Add(Position.RightUp, oneExceptD);

				return notInD.Single().Key;
                
			}
			else if (eIn == 9)
			{            
                var oneExceptE = one.Except(new[] { 'e' }).Single();
				knownCharPositions.Add(Position.RightUp, oneExceptE);

				return notInE.Single().Key;
                
			}
			else if (fIn == 9)
			{
                var oneExceptF = one.Except(new[] { 'f' }).Single();
				knownCharPositions.Add(Position.RightUp, oneExceptF);

				return notInF.Single().Key;
                
			} 
			else
			{
                var oneExceptG = one.Except(new[] { 'g' }).Single();
                knownCharPositions.Add(Position.RightUp, oneExceptG);

				return notInG.Single().Key;
                
			}
		}

		private static string FindUniqueLengthElement(string[] currentInputs, int number)
		{
			if (number == 8)
            {
				return currentInputs.First(input => input.Length == 7);
            }
            else if (number == 7)
            {
				return currentInputs.First(input => input.Length == 3);
            }
            else if (number == 4)
            {
				return currentInputs.First(input => input.Length == 4);
            }
            else if (number == 1)
            {
				return currentInputs.First(input => input.Length == 2);
            }

			return string.Empty;
		}

		public enum Position {
            RightUp,
		} 
	}
}
