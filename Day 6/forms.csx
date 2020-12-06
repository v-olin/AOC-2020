using System;
using System.IO;
using System.Linq;

string forms = "forms.txt";
string[] lines = File.ReadAllLines(forms);

var groups = new List<List<string>>() { new List<string>() };
int j = 0;
for (int i = 0; i < lines.Length; i++){
    if (lines[i] == ""){
        groups.Add(new List<string>());
        j++;
    }
    else
        groups[j].Add(lines[i]);
}
int sum = groups
    .Select(l => l.Aggregate((tail, next) => new String(tail.Intersect(next).ToArray())).Count()).ToList()
    .Sum();
Console.WriteLine($"Part 2: {sum}")