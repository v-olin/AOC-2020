using System;
using System.IO;
using System.Linq;

List<int> adapters = File.ReadAllLines("adapters.txt").Select(s => int.Parse(s)).ToList();
adapters.Add(0);
adapters.Sort();
adapters.Add(adapters.Last() + 3);

var diffs = new Dictionary<int, int>();

for(int i = 0; i < adapters.Count() - 1; i++){
    int diff = adapters[i+1] - adapters[i];
    if (diffs.ContainsKey(diff))
        diffs[diff]++;
    else
        diffs.Add(diff, 1);        
}

int prod = diffs[1] * diffs[3];
Console.WriteLine($"Part 1: {prod}");