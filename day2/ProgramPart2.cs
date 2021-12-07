using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"C:\aoc_day2.txt");
            var depth = 0;
            var horizontalPosition = 0;
            var aim = 0;

            foreach (string line in lines)
            {
                var splitLine = line.Split(' ');
                var direction = Enum.Parse<Direction>(splitLine[0]);
                var units = int.Parse(splitLine[1]);

                if (direction == Direction.down)
                {
                    aim += units;
                }
                else if (direction == Direction.forward)
                {
                    horizontalPosition += units;
                    depth += (aim * units);
                }
                else
                {
                    aim -= units;
                }
            }

            Console.WriteLine($"Depth: {depth}\nPosition: {horizontalPosition}\nAim: {aim}\nProduct: {depth * horizontalPosition}");
        }

        private enum Direction
        {
            forward,
            down,
            up
        }
    }
}
