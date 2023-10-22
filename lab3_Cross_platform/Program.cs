using System;
using System.Collections.Generic;
using System.IO;
using static lab3_Algorithm; //імпорт створеного пакету

public class lab3_Cross_platform
{ 
    public static void Main()
    {
        try
        {
            string[] lines = File.ReadAllLines("INPUT.TXT");

            if (lines.Length == 0) // чи вхідний файл порожній
            {
                Console.WriteLine("The input file is empty.");
                return;
            }

            string[] parts = lines[0].Split(' '); // розділяємо перший рядок файлу пробілами
            if (parts.Length != 2) // чи перший рядок файлу містить рівно два цілі числа
            {
                Console.WriteLine("The first line of the input file should contain two integers.");
                return;
            }

            int n = int.Parse(parts[0]);
            int m = int.Parse(parts[1]);

            if (n <= 0 || n >= 1000 || m < 0 || m > 100000) // чи цілі числа знаходяться в заданому діапазоні
            {
                Console.WriteLine("The number of settlements and/or roads is out of the allowed range.");
                return;
            }

            if (lines.Length != m + 1) // чи відповідає кількість рядків у файлі очікуваній кількості доріг
            {
                Console.WriteLine("The number of roads described doesn't match the provided count.");
                return;
            }

            List<Tuple<int, int>> edges = new List<Tuple<int, int>>(); // список для зберігання ребер графа
            for (int i = 1; i <= m; i++) // розбір кожного рядка файлу для вилучення ребер

            {
                parts = lines[i].Split(' ');
                if (parts.Length != 2) //  чи  описує рядок дорогу між двома населеними пунктами
                {
                    Console.WriteLine($"Line {i + 1} is expected to describe a road between two settlements.");
                    return;
                }

                int v = int.Parse(parts[0]);
                int u = int.Parse(parts[1]);

                if (v <= 0 || u <= 0 || v > n || u > n) // чи знаходяться населені пункти в допустимому діапазоні
                {
                    Console.WriteLine($"Line {i + 1} describes an invalid road. Settlement numbers should be between 1 and {n}.");
                    return;
                }

                edges.Add(new Tuple<int, int>(v, u)); // додати розібране ребро до списку ребер
            }
            
            int result = MinMercenaries(n, m, edges); //використання створеного пакету. метод MinMercenaries з класу lab3_Algorithm

            File.WriteAllText("OUTPUT.TXT", result.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

}