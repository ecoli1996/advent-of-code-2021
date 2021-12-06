using System;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"C:\aoc_day2.txt").Select(line => GetLine(line)).ToArray();
            var depth = 0;
            var horizontalPosition = 0;

            foreach (Tuple<Direction, int> line in lines)
            {
                if (line.Item1 == Direction.down) depth += line.Item2;
                else if (line.Item1 == Direction.forward) horizontalPosition += line.Item2;
                else depth -= line.Item2;
            }

            Console.WriteLine($"Depth: {depth}\nPosition: {horizontalPosition}\nProduct: {depth * horizontalPosition}");
        }

        private static Tuple<Direction, int> GetLine(string line)
        {
            var lineComponents = line.Split(' ');
            var direction = Enum.Parse<Direction>(lineComponents[0]);
            var units = int.Parse(lineComponents[1]);
            return new Tuple<Direction, int>(direction, units);
        }

        private enum Direction
        {
            forward,
            down,
            up
        }
    }
}
