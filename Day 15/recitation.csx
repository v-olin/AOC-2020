using System;
using System.IO;
using System.Linq;

string input = "13,16,0,12,15,1";
var saidNums = input.Split(',').Select(int.Parse);
List<int> nums = GetNumbers(saidNums)
    .Take(30000000 - saidNums.Count())
    .ToList();
Console.WriteLine($"Part 1: {nums[2020 - saidNums.Count() - 1]}\nPart 2: {nums.Last()}");

IEnumerable<int> GetNumbers(IEnumerable<int> initNums){
    List<int> pastNums = initNums.ToList();
    var pastNumOccs = new Dictionary<int, List<int>>();
    for (int i = 0; i < pastNums.Count; i++)
        AddNumOccurrance(ref pastNumOccs, pastNums[i], i);
    while (true){
        int lastNum = pastNums.Last();
        if (pastNumOccs[lastNum].Count > 1){
            List<int> previousIndexes = pastNumOccs[lastNum];
            int amountOfIndexes = previousIndexes.Count;
            int n = previousIndexes[amountOfIndexes - 1] - previousIndexes[amountOfIndexes - 2];
            pastNums.Add(n);
            yield return (n);
        }
        else if (!pastNumOccs.ContainsKey(lastNum) || pastNumOccs[lastNum].Count < 2){
            pastNums.Add(0);
            yield return 0;
        }
        AddNumOccurrance(ref pastNumOccs, pastNums.Last(), pastNums.Count - 1);
    }
}

void AddNumOccurrance(ref Dictionary<int, List<int>> d, int n, int i){
    if (d.ContainsKey(n)) d[n].Add(i);
    else d.Add(n, new List<int>() {i});
}