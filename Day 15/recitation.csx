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
    List<int> nums = initNums.ToList();
    var numsSeen = new Dictionary<int, List<int>>();
    for (int i = 0; i < nums.Count; i++)
        NumSeen(ref numsSeen, nums[i], i);
    while (true){
        int lastNum = nums.Last();
        if (numsSeen[lastNum].Count > 1){
            List<int> pis = numsSeen[lastNum];
            int nIs = pis.Count;
            int n = pis[nIs - 1] - pis[nIs - 2];
            nums.Add(n);
            yield return n;
        }
        else if (!numsSeen.ContainsKey(lastNum) || numsSeen[lastNum].Count < 2){
            nums.Add(0);
            yield return 0;
        }
        NumSeen(ref numsSeen, nums.Last(), nums.Count - 1);
    }
}

void NumSeen(ref Dictionary<int, List<int>> d, int n, int i){
    if (d.ContainsKey(n)) d[n].Add(i);
    else d.Add(n, new List<int>() {i});
}