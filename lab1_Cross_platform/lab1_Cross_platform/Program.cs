using System;
using System.Collections.Generic;
 
class BackpackProblem
{
    static long maxUtility = 0;
    static int minCount = int.MaxValue;
    static List<int> selectedItems = new List<int>();
    static int n, W;
    static int[] weights, utilities;
 
    static void Search(int index, int currentWeight, long currentUtility, int currentCount, List<int> currentItems)
    {
        // Check base cases
        if (currentWeight > W) return;
         
        // Check if the current configuration is a better solution
        if (currentUtility > maxUtility || (currentUtility == maxUtility && currentCount < minCount))
        {
            maxUtility = currentUtility;
            minCount = currentCount;
            selectedItems = new List<int>(currentItems);
        }
         
        if (index == n) return; // Stop if all items have been considered.
 
        // Include the current item
        currentItems.Add(index + 1); // +1 because items are 1-indexed in the output
        Search(index + 1, currentWeight + weights[index], currentUtility + utilities[index], currentCount + 1, currentItems);
        currentItems.RemoveAt(currentItems.Count - 1); // Backtrack
         
        // Exclude the current item
        Search(index + 1, currentWeight, currentUtility, currentCount, currentItems);
    }
 
    static void Main()
    {
        // Read n and W
        var input = Console.ReadLine().Split(' ');
        n = int.Parse(input[0]);
        W = int.Parse(input[1]);
 
        // Initialize and read weights and utilities
        weights = new int[n];
        utilities = new int[n];
        for (int i = 0; i < n; i++)
        {
            var item = Console.ReadLine().Split(' ');
            weights[i] = int.Parse(item[0]);
            utilities[i] = int.Parse(item[1]);
        }
 
        // Start the search
        Search(0, 0, 0, 0, new List<int>());
         
        // Output results
        Console.WriteLine($"{minCount} {maxUtility}");
        Console.WriteLine(string.Join(" ", selectedItems));
    }
}