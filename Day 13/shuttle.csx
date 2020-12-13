using System;
using System.IO;
using System.Linq;

string[] input = File.ReadAllLines("timetable.txt");
long timestamp = long.Parse(input[0]);
List<Bus> shuttles = input[1]
    .Split(',')
    .Where(b => b != "x")
    .Select(s => new Bus(){
        ID = long.Parse(s),
        WaitTime = (long.Parse(s) - (timestamp % long.Parse(s))),
        Index = input[1].Split(',').ToList().IndexOf(s)
    })
    .ToList();

Bus earliestBus = shuttles
    .OrderBy(b => b.WaitTime)
    .FirstOrDefault();

Console.WriteLine($"Part 1: {earliestBus.ID * earliestBus.WaitTime}");

List<Equation> set = shuttles.Select(b => new Equation(){
    a = b.ID - b.Index,
    n = b.ID
}).ToList();

long bigN = set.Aggregate((t,n) => new Equation() { a = t.a, n = t.n*n.n }).n;

for (int i = 0; i < set.Count(); i++){
    set[i].np = bigN / set[i].n;
    for (int p = 1; set[i].u == 0; p++)
        set[i].u = p*set[i].np % set[i].n == 1 ? p : 0;
}

long ans = set.Select(e => e.a*e.np*e.u).Sum() % bigN;
Console.WriteLine($"Part 2: {ans}");

public class Bus {
    public long ID { get; set; }
    public long WaitTime { get; set; }
    public int Index { get; set; }
}

public class Equation {
    public long a { get; set; }
    public long n { get; set; }
    public long np { get; set; }
    public long u { get; set; }
}