using System;
using System.IO;
using System.Linq;

string fp = "nums.txt";
List<long> nums = File.ReadAllLines(fp).Select(s => long.Parse(s)).ToList();
List<long> preamble = nums.Take(25).ToList();
long? misfit;

for (int i = preamble.Count(); i < nums.Count(); i++){
    long b = 0;
    for (int j = 0; j < preamble.Count(); j++){
        long a = preamble[j];
        preamble.RemoveAt(j);
        if (b == 0){
            b = preamble.Find(m => m + a == nums[i]);
        }
        else{
            preamble.Insert(j, a);
            break;
        }
        preamble.Insert(j, a);
    }
    if (b == 0){
        misfit = nums[i];
        Console.WriteLine($"Misfit: {misfit}");
        break;
    }
    preamble = preamble.Skip(1).Append(nums[i]).ToList();
}

preamble = nums.Take(25).ToList();

var contiguousSet = new List<long>();
for (int i = preamble.Count(); i < nums.Count(); i++){
    // long b = 0;
    int preamblePtr = 0;
    while (contiguousSet.Sum() < misfit){
        if (preamblePtr < preamble.Count()){
            contiguousSet.Add(preamble[preamblePtr]);
            preamblePtr++;
        }
        else{
            break;
        }
    }
    if (contiguousSet.Sum() == misfit){
        long cmin = contiguousSet.Min();
        long cmax = contiguousSet.Max();
        Console.WriteLine($"Part 2: {cmin+cmax}");
        break;
    }
    else{
        preamblePtr = 0;
        contiguousSet.Clear();
    }
    preamble = preamble.Skip(1).Append(nums[i]).ToList();
}