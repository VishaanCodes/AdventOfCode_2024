using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_Day5_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string rulesFilePath = "rules.txt", 
                   updatesFilePath = "updates.txt"; 
            string[] rules = File.ReadAllLines(rulesFilePath),
                     updates = File.ReadAllLines(updatesFilePath);
            var graph = BuildGraph(rules);
            int sumOfMiddlePages = 0;
            foreach (var updateLine in updates)
            {
                var update = updateLine.Split(',').Select(int.Parse).ToList();
                if (IsCorrectOrder(update, graph))
                {
                    int middleIndex = update.Count / 2;
                    sumOfMiddlePages += update[middleIndex];
                }
            }
            Console.WriteLine("Sum of middle pages: " + sumOfMiddlePages);
            Console.ReadLine();
        }
        static Dictionary<int, List<int>> BuildGraph(string[] rules)
        {
            var graph = new Dictionary<int, List<int>>();
            foreach (var rule in rules)
            {
                var parts = rule.Split('|');
                int from = int.Parse(parts[0]),
                    to = int.Parse(parts[1]);
                if (!graph.ContainsKey(from))
                {
                    graph[from] = new List<int>();
                }
                graph[from].Add(to);
            }
            return graph;
        }
        static bool IsCorrectOrder(List<int> update, Dictionary<int, List<int>> graph)
        {
            var position = new Dictionary<int, int>();
            for (int i = 0; i < update.Count; i++)
            {
                position[update[i]] = i;
            }
            foreach (var from in graph.Keys)
            {
                if (!position.ContainsKey(from)) continue;
                foreach (var to in graph[from])
                {
                    if (position.ContainsKey(to) && position[from] > position[to])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
