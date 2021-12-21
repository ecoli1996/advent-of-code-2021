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
			var lines = System.IO.File.ReadAllLines(@"/Users/evc1996/Projects/AdventOfCode/AdventOfCode/aoc_day12.txt");
			var nodeConnections = new Dictionary<string, NodeInfo> { { "start", new NodeInfo { Children = new List<string>(), Ends = false } } };


			foreach (string line in lines)
			{
				var vertex = line.Split('-');
				var node1 = vertex[0];
				var node2 = vertex[1];

				var node1Start = string.Equals("start", node1);
				var node2Start = !node1Start && string.Equals("start", node2);
				var node1End = !node1Start && string.Equals("end", node1);
				var node2End = !node2Start && string.Equals("end", node2);

				if (node2Start)
				{
					nodeConnections[node2].Children.Add(node1);
				}
				else if (node1Start)
				{
					nodeConnections[node1].Children.Add(node2);
				}            
				else if (node1End)
				{
					if (nodeConnections.ContainsKey(node2)) nodeConnections[node2].Ends = true;
					else nodeConnections.Add(node2, new NodeInfo { Children = new List<string>(), Ends = true } );
				}
				else if (node2End)
				{
					if (nodeConnections.ContainsKey(node1)) nodeConnections[node1].Ends = true;
                    else nodeConnections.Add(node1, new NodeInfo { Children = new List<string>(), Ends = true });
				}
				else
				{
					if (nodeConnections.ContainsKey(node1)) nodeConnections[node1].Children.Add(node2);
					else nodeConnections.Add(node1, new NodeInfo { Children = new List<string> { node2 }, Ends = false });

					if (nodeConnections.ContainsKey(node2)) nodeConnections[node2].Children.Add(node1);
                    else nodeConnections.Add(node2, new NodeInfo { Children = new List<string> { node1 }, Ends = false });
				}
			}
         
			foreach (NodeInfo nodeInfo in nodeConnections.Values)
			{
				if (nodeInfo.Ends) nodeInfo.Children.Add("end");
			}

			List<List<string>> paths = new List<List<string>>();
			foreach (string child in nodeConnections["start"].Children)
			{
				var currentPath = new List<string> { "start" };
				BuildPaths(child, currentPath, nodeConnections, paths, true);
			}

			Console.WriteLine($"num of paths: {paths.Count}");
			var count = 1;
			foreach(List<string> path in paths)
			{
				var sb = new StringBuilder();
				sb.Append($"Count: {count}... {{ ");
				foreach(string node in path)
				{
					sb.Append($"{node},");
				}
				sb.Append(" }");
				Console.WriteLine(sb.ToString());
				count++;
			}
		}

		private static void BuildPaths(string currentNode, List<string> currentPath, Dictionary<string, NodeInfo> nodeConnections, List<List<string>> paths, bool allowExtraSmallCaveVisit)
		{
			currentPath.Add(currentNode);
			if (string.Equals(currentNode, "end")) paths.Add(currentPath);
			else
			{
				foreach(string child in nodeConnections[currentNode].Children)
				{
					if (LowerCaseChildVisited(child, currentPath))
					{
						if (!allowExtraSmallCaveVisit)
						{
                            continue;
						}

                        var currentPathCopy = currentPath.Select(x => x).ToList();
                        BuildPaths(child, currentPathCopy, nodeConnections, paths, false);
					}
					else
					{
						var currentPathCopy = currentPath.Select(x => x).ToList();
						BuildPaths(child, currentPathCopy, nodeConnections, paths, allowExtraSmallCaveVisit);
					}
				}
			}
		}

		private static bool LowerCaseChildVisited(string child, List<string> currentPath)
		{
			if (string.Equals(child, "start") || string.Equals(child, "end") || !string.Equals(child, child.ToLower())) return false;         
			return currentPath.FirstOrDefault(c => c == child) != null;

		}

		public class NodeInfo
		{
			public List<string> Children { get; set; }
			public bool Ends { get; set; }
		}      
	}
}
