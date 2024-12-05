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
            string[] rules = File.ReadAllLines("rules.txt");
            string[] updates = File.ReadAllLines("updates.txt");
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            foreach (string rule in rules)
            {
                string[] parts = rule.Split('|');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int before) &&
                    int.TryParse(parts[1], out int after))
                {
                    if (!graph.ContainsKey(before))
                    {
                        graph[before] = new List<int>();
                    }
                    graph[before].Add(after);
                }
            }
            List<int> incorrectMiddlePages = new List<int>();
            foreach (string update in updates)
            {
                if (string.IsNullOrWhiteSpace(update))
                {
                    continue;
                }
                List<int> pages = update.Split(',')
                                        .Select(p => int.TryParse(p, out int page) ? page : -1)
                                        .Where(page => page != -1)
                                        .ToList();
                if (pages.Count == 0)
                {
                    continue;
                }
                if (IsCorrectlyOrdered(pages, graph))
                {
                    continue;
                }
                List<int> sortedPages = TopologicalSort(pages, graph);
                if (sortedPages.Count == pages.Count)
                {
                    incorrectMiddlePages.Add(sortedPages[sortedPages.Count / 2]);
                }
            }
            int sumOfMiddlePages = incorrectMiddlePages.Sum();
            Console.WriteLine(sumOfMiddlePages);
            Console.ReadLine();
        }
        static bool IsCorrectlyOrdered(List<int> pages, Dictionary<int, List<int>> graph)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                for (int j = i + 1; j < pages.Count; j++)
                {
                    if (graph.ContainsKey(pages[j]) && graph[pages[j]].Contains(pages[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        static List<int> TopologicalSort(List<int> pages, Dictionary<int, List<int>> graph)
        {
            Dictionary<int, List<int>> filteredGraph = graph
                .Where(kvp => pages.Contains(kvp.Key))
                .ToDictionary(kvp => kvp.Key,
                              kvp => kvp.Value.Where(v => pages.Contains(v)).ToList());
            Dictionary<int, int> inDegree = pages.ToDictionary(page => page, _ => 0);
            foreach (var kvp in filteredGraph)
            {
                foreach (int neighbor in kvp.Value)
                {
                    inDegree[neighbor]++;
                }
            }
            Queue<int> queue = new Queue<int>();
            foreach (int page in pages)
            {
                if (inDegree[page] == 0)
                {
                    queue.Enqueue(page);
                }
            }
            List<int> sortedList = new List<int>();
            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                sortedList.Add(current);
                if (filteredGraph.ContainsKey(current))
                {
                    foreach (int neighbor in filteredGraph[current])
                    {
                        inDegree[neighbor]--;
                        if (inDegree[neighbor] == 0)
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
            return sortedList;
        }
    }
}
