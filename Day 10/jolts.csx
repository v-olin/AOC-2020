using System;
using System.IO;
using System.Linq;

List<long> adapters = File.ReadAllLines("adapters.txt").Select(long.Parse).ToList();
adapters.Add(0);
adapters.Sort();
adapters.Add(adapters.Last() + 3);

var diffs = new Dictionary<long, long>();

for(int i = 0; i < adapters.Count() - 1; i++){
    long diff = adapters[i+1] - adapters[i];
    if (diffs.ContainsKey(diff))
        diffs[diff]++;
    else
        diffs.Add(diff, 1);
}

Console.WriteLine($"Part 1: {diffs[1] * diffs[3]}");

adapters.Reverse();
diffs.Clear();
foreach (long jolt in adapters){
    diffs[jolt] = adapters
        .Where(j => j > jolt && j <= jolt + 3)
        .Select(n => diffs[n])
        .Sum();
    if (diffs[jolt] == 0)
        diffs[jolt] = 1;
}

Console.WriteLine($"Part 2: {diffs[0]}");