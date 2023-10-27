using System;
using System.Collections.Generic;
using System.IO;
namespace LabClassLibrary
{
    public static class Lab3
    {
        private static void Dfs(int v, List<int>[] adj, bool[] visited, List<int> oddVertices)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(v);
            while (stack.Count > 0)
            {
                int vertex = stack.Pop();
                if (!visited[vertex])
                {
                    visited[vertex] = true;
                    if (adj[vertex].Count % 2 == 1)
                        oddVertices.Add(vertex);
                    foreach (int neighbor in adj[vertex])
                        if (!visited[neighbor])
                            stack.Push(neighbor);
                }
            }
        }
     
        public static int MinMercenaries(int n, int m, List<Tuple<int, int>> edges)
        {
            List<int>[] adj = new List<int>[n + 1];
            for (int i = 0; i <= n; i++)
                adj[i] = new List<int>();

            foreach (var edge in edges)
            {
                adj[edge.Item1].Add(edge.Item2);
                adj[edge.Item2].Add(edge.Item1);
            }
     
            bool[] visited = new bool[n + 1];
            int ans = 0;
     
            for (int i = 1; i <= n; i++)
            {
                if (!visited[i]) 
                {
                    List<int> oddVertices = new List<int>();
                    Dfs(i, adj, visited, oddVertices); 
                    if (oddVertices.Count % 2 == 1) 
                        ans += oddVertices.Count / 2 + 1;
                    else
                        ans += oddVertices.Count / 2;

                    if (oddVertices.Count == 0 && adj[i].Count > 0)
                        ans += 1;
                }
            }
            return ans;
        }


        public static void ExecuteLab3(string inputFilePath, string outputFilePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(inputFilePath);

                if (lines.Length == 0)
                {
                    Console.WriteLine("The input file is empty.");
                    return;
                }

                string[] parts = lines[0].Split(' ');
                if (parts.Length != 2)
                {
                    Console.WriteLine("The first line of the input file should contain two integers.");
                    return;
                }

                int n = int.Parse(parts[0]);
                int m = int.Parse(parts[1]);

                if (n <= 0 || n >= 1000 || m < 0 || m > 100000)
                {
                    Console.WriteLine("The number of settlements and/or roads is out of the allowed range.");
                    return;
                }

                if (lines.Length != m + 1)
                {
                    Console.WriteLine("The number of roads described doesn't match the provided count.");
                    return;
                }

                List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
                for (int i = 1; i <= m; i++)
                {
                    parts = lines[i].Split(' ');
                    if (parts.Length != 2) 
                    {
                        Console.WriteLine($"Line {i + 1} is expected to describe a road between two settlements.");
                        return;
                    }

                    int v = int.Parse(parts[0]);
                    int u = int.Parse(parts[1]);

                    if (v <= 0 || u <= 0 || v > n || u > n) 
                    {
                        Console.WriteLine($"Line {i + 1} describes an invalid road. Settlement numbers should be between 1 and {n}.");
                        return;
                    }

                    edges.Add(new Tuple<int, int>(v, u)); 
                }
                
                int result = MinMercenaries(n, m, edges); 

                File.WriteAllText(outputFilePath, result.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }

}