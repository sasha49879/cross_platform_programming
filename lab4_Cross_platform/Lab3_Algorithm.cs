using System;
using System.Collections.Generic;

public class lab3_Algorithm
{
    // метод для пошуку в глибину
    private static void DFS(int v, List<int>[] adj, bool[] visited, List<int> oddVertices)
    {
        Stack<int> stack = new Stack<int>(); // порожній стек для зберігання вершин
        stack.Push(v); // переміщення початкової вершини у стек
        while (stack.Count > 0) // продовжувати до тих пір, поки у стеку є вершини
        {
            int vertex = stack.Pop(); // витягнути верхню вершину зі стеку
            if (!visited[vertex]) // якщо вершину ще не відвідували
            {
                visited[vertex] = true; // позначити вершину як відвідану
                if (adj[vertex].Count % 2 == 1)  // якщо вершина має непарний степінь (тобто непарну кількість сусідів)
                {
                    oddVertices.Add(vertex);
                }
                foreach (int neighbor in adj[vertex]) // перебір всіх сусідів вершини
                {
                    if (!visited[neighbor]) // якщо сусід ще не був відвіданий
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }
    }
 
    //  метод для знаходження мінімальної кількості найманців
    public static int MinMercenaries(int n, int m, List<Tuple<int, int>> edges)
    {
        List<int>[] adj = new List<int>[n + 1];  // список суміжності для графа
        for (int i = 0; i <= n; i++) // ініціалізувати список суміжності
        {
            adj[i] = new List<int>();
        }
 
        foreach (var edge in edges) // заповнити список суміжності, використовуючи надані ребра
        {
            adj[edge.Item1].Add(edge.Item2);
            adj[edge.Item2].Add(edge.Item1);
        }
 
        bool[] visited = new bool[n + 1]; //масив для відстеження відвіданих вершин
        int ans = 0; // ініціалізувати відповідь
 
        for (int i = 1; i <= n; i++) // прохід по всіх вершинах
        {
            if (!visited[i]) // якщо вершину ще не відвідували
            {
                List<int> oddVertices = new List<int>(); // список для зберігання вершин з непарним степенем
                DFS(i, adj, visited, oddVertices); // виконати DFS, починаючи з поточної вершини.
 
                if (oddVertices.Count % 2 == 1) // якщо кількість вершин непарного степеня в компоненті непарна
                {
                    ans += oddVertices.Count / 2 + 1;
                }
                else
                {
                    ans += oddVertices.Count / 2;
                }
 
                if (oddVertices.Count == 0 && adj[i].Count > 0) //  якщо немає вершин з непарним степенем, але у вершини є сусіди
                {
                    ans += 1;
                }
            }
        }
        return ans;
    }
}