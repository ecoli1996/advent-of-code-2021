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
            var lines = System.IO.File.ReadAllLines(@"C:\aoc_day13.txt");
            var coordinates = new List<Tuple<int, int>>();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) { }
                else if (line[0] == 'f')
                {
                    var directionsSplit = line.Split(' ');
                    var yOrXEquals = directionsSplit.Last().Split('=');
                    var axis = yOrXEquals.First()[0];
                    var foldPoint = int.Parse(yOrXEquals.Last());

                    var direction = new FoldDirection
                    {
                        Axis = axis == 'x' ? Axis.X : Axis.Y,
                        FoldPoint = foldPoint
                    };

                    for (int i = 0; i < coordinates.Count; i++)
                    {
                        var coordinate = coordinates[i];
                        if (CoordinateWillFold(coordinate, direction))
                        {
                            coordinates[i] = direction.Axis == Axis.X ? TransformX(coordinate, direction.FoldPoint) : TransformY(coordinate, direction.FoldPoint);
                        }
                    }
                }
                else
                {
                    var coordinate = line.Split(',');
                    var x = int.Parse(coordinate[0]);
                    var y = int.Parse(coordinate[1]);
                    coordinates.Add(new Tuple<int, int>(x, y));
                }
            }

            var dotCoordinates = coordinates
                .Where(c => c != null && c.Item1 >= 0 && c.Item2 >= 0)
                .Distinct().ToDictionary(val => val, val => '#');

            var maxX = dotCoordinates.Keys.OrderByDescending(k => k.Item1).First().Item1;
            var maxY = dotCoordinates.Keys.OrderByDescending(k => k.Item2).First().Item2;

            var currentX = 0;
            var currentY = 0;
            var sb = new StringBuilder();
            while (currentY <= maxY)
            {
                var currentCoordinate = new Tuple<int, int>(currentX, currentY);
                if (dotCoordinates.ContainsKey(currentCoordinate)) sb.Append('#');
                else sb.Append('.');

                currentX++;
                if (currentX > maxX)
                {
                    currentX = 0;
                    currentY++;
                    sb.Append('\n');
                }
            }

            Console.Write(sb.ToString());
        }

        private static Tuple<int, int> TransformY(Tuple<int, int> coordinate, int foldPoint)
        {
            var newY = (2 * foldPoint) - coordinate.Item2;
            return new Tuple<int, int>(coordinate.Item1, newY);
        }

        private static Tuple<int, int> TransformX(Tuple<int, int> coordinate, int foldPoint)
        {
            var newX = (2 * foldPoint) - coordinate.Item1;
            return new Tuple<int, int>(newX, coordinate.Item2);
        }

        private static bool CoordinateWillFold(Tuple<int, int> coordinate, FoldDirection direction)
        {
            if (coordinate == null) return false;
            if (direction.Axis == Axis.X) return coordinate.Item1 > direction.FoldPoint;
            return coordinate.Item2 > direction.FoldPoint;
        }

        public class FoldDirection
        {
            public Axis Axis { get; set; }
            public int FoldPoint { get; set; }
        }

        public enum Axis
        {
            X,
            Y
        }
    }
}
