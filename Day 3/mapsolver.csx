using System;
using System.IO;
using System.Linq;

string mapTemplate = "map.txt";
string[] rows = File.ReadAllLines(mapTemplate);
var treesTraversed = new List<Int64>();
var slopes = new List<(int r, int d)>(){
    (1,1),
    (3,1),
    (5,1),
    (7,1),
    (1,2)
};

foreach ((int r, int d) in slopes){
    int j = 0;
    int n = 0;
    for (int i = 0; i < rows.Length; i = i + d){
        if (rows[i][j] == '#')
            n++;
        j = (j + r) % rows[0].Length;
    }
    treesTraversed.Add((Int64)n);
}

Int64 zip = treesTraversed.Aggregate((tail, next) => tail * next);
Console.WriteLine($"Trees traversed: {zip}")